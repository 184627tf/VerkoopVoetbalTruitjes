using Domein;
using Domein.Interfaces;
using Domein.Model;
using System;
using System.Collections.Generic;
using System.Data;
using static SQLserver.Repositories.Repository;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLserver.Repositories {
    public class KlantRepository : IKlantRepository {
        string _connectionString = Config.ConnectionString;

        public int VoegKlantToe(Klant klant) {
            return (int)VoerDBActieUit(c => VoegKlantToe(klant, c));
        }

        public static int VoegKlantToe(Klant klant, IDbCommand command) {
            if (AdresRepository.AdresExists(klant.Adres, command)) {
                klant.Adres = AdresRepository.GeefAdres(klant.Adres.Adresnaam, command);
            } else {
                klant.Adres.Id = AdresRepository.VoegAdresToe(klant.Adres, command);
            }

            command.CommandText = "INSERT INTO Klant (naam, adres_id) OUTPUT INSERTED.id VALUES (@naam, @adres_id);";
            command.Parameters.Add(new SqlParameter("naam", klant.Naam));
            command.Parameters.Add(new SqlParameter("adres_id", klant.Adres.Id));
            return (int)command.ExecuteScalar();
        }

        public IEnumerable<Klant> GeefKlanten() {
            return (IEnumerable<Klant>)VoerDBActieUit(c => GeefKlanten(c));
        }

        public static IEnumerable<Klant> GeefKlanten(IDbCommand command) {
            List<Klant> klanten = new List<Klant>();
            command.CommandText = "SELECT k.id AS id, k.naam AS naam, a.id AS adresid, a.adres AS adres FROM Klant k INNER JOIN Adres a ON k.adres_id = a.id;";

            SqlDataReader reader = (SqlDataReader)command.ExecuteReader();
            while (reader.Read()) {
                klanten.Add(ParseKlant(reader));
            }

            reader.Close();

            return klanten.AsEnumerable();
        }

        public void UpdateKlant(Klant klant) {
            VoerDBActieUit(c => UpdateKlant(klant, c));
        }
        public static void UpdateKlant(Klant klant, IDbCommand command) {
            if (AdresRepository.AdresExists(klant.Adres, command)) {
                klant.Adres.Id = AdresRepository.GeefAdres(klant.Adres.Adresnaam, command).Id;
            } else {
                klant.Adres.Id = AdresRepository.VoegAdresToe(klant.Adres, command);
            }
            command.CommandText = "UPDATE Klant SET naam=@naam, adres_id=@adresId WHERE id=@id;";
            command.Parameters.Add(new SqlParameter("id", klant.Id));
            command.Parameters.Add(new SqlParameter("naam", klant.Naam));
            command.Parameters.Add(new SqlParameter("adresId", klant.Adres.Id));

            command.ExecuteNonQuery();
        }
        
        public void VerwijderKlant(Klant klant) {
            VoerDBActieUit(c => VerwijderKlant(klant, c));
        }

        public static void VerwijderKlant(Klant klant, IDbCommand command) {
            command.CommandText = "DELETE FROM Klant WHERE id=@id;";
            IDataParameter idParameter = new SqlParameter("id", klant.Id);
            command.Parameters.Add(idParameter);

            command.ExecuteNonQuery();
        }

        public bool Exists(Klant klant) {
            return (bool)VoerDBActieUit(c => KlantExists(klant, c));
        }

        public static bool KlantExists(Klant klant, IDbCommand command) {
            command.CommandText = "SELECT COUNT(*) FROM Klant WHERE id=@id;";
            IDataParameter idParameter = new SqlParameter("id", klant.Id);
            command.Parameters.Add(idParameter);

            int amount = (int)command.ExecuteScalar();

            return amount > 0;
        }

        private static Klant ParseKlant(SqlDataReader reader) {
            int klantnummer = (int)reader["id"];
            string naam = (string)reader["naam"];
            int adresid = (int)reader["adresid"];
            string adres = (string)reader["adres"];

            return new Klant(klantnummer, naam, new Adres(adresid, adres));
        }
    }
}
