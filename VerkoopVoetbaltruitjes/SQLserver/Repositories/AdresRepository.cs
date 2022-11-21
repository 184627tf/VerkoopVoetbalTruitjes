using Domein;
using Domein.Interfaces;
using Domein.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static SQLserver.Repositories.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLserver.Repositories {
    public class AdresRepository : IAdresRepository {
        string _connectionString = Config.ConnectionString;

        public IEnumerable<Adres> GeefAdressen() {
            return (IEnumerable<Adres>)VoerDBActieUit(c => GeefAdressen(c));
        }
        
        public static IEnumerable<Adres> GeefAdressen(IDbCommand command) {
            List<Adres> adressen = new List<Adres>();
            command.CommandText = "SELECT * FROM Adres;";

            IDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                adressen.Add(ParseAdres(reader));
            }

            reader.Close();

            return adressen.AsEnumerable();
        }

        public void UpdateAdres(Adres adres) {
            VoerDBActieUit(c => UpdateAdres(adres, c));
        }

        public static void UpdateAdres(Adres adres, IDbCommand command) {
            command.CommandText = "UPDATE Adres SET adres=@adres WHERE id=@id;";
            command.Parameters.Add(new SqlParameter("id", adres.Id));
            command.Parameters.Add(new SqlParameter("adres", adres.Adresnaam));

            command.ExecuteNonQuery();
        }

        public void VerwijderAdres(int id) {
            VoerDBActieUit(c => VerwijderAdres(id, c));
        }

        public static void VerwijderAdres(int id, IDbCommand command) {
            command.CommandText = "DELETE FROM Adres WHERE id=@id";
            command.Parameters.Add(new SqlParameter("id", id));

            command.ExecuteNonQuery();
        }

        public int VoegAdresToe(Adres adres) {
            return (int)VoerDBActieUit(c => VoegAdresToe(adres, c));
        }

        public static int VoegAdresToe(Adres adres, IDbCommand command) {
            command.CommandText = "INSERT INTO Adres (adres) OUTPUT INSERTED.id VALUES (@adres);";
            command.Parameters.Add(new SqlParameter("adres", adres.Adresnaam));
            int id = (int)command.ExecuteScalar();
            return id;
        }

        public bool AdresExists(Adres adres) {
            return (bool)VoerDBActieUit(c => AdresExists(adres, c));
        }

        public static bool AdresExists(Adres adres, IDbCommand command) {
            command.CommandText = "SELECT COUNT(*) FROM Adres WHERE adres=@adresnaam;";
            IDataParameter adresParameter = new SqlParameter("adresnaam", adres.Adresnaam);
            command.Parameters.Add(adresParameter);

            int amount = (int)command.ExecuteScalar();
            
            return amount > 0;
        }

        public Adres GeefAdres(string adres) {
            return (Adres)VoerDBActieUit(c => GeefAdres(adres, c));
        }

        public static Adres GeefAdres(string adres, IDbCommand command) {
            command.CommandText = "SELECT TOP 1 id FROM Adres WHERE adres=@adres;";
            command.Parameters.Add(new SqlParameter("adres", adres));
            int adresId = (int)command.ExecuteScalar();
            return new Adres(adresId, adres);
        }

        private static Adres ParseAdres(IDataReader reader) {
            int? id = (int)reader["id"];
            string adres = (string)reader["adres"];

            return new Adres(id, adres);
        }
    }
}
