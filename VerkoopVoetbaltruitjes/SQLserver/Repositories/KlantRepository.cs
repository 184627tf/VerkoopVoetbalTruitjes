using Domein;
using Domein.Interfaces;
using Domein.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLserver.Repositories {
    public class KlantRepository : IKlantRepository {
        string _connectionString = Config.ConnectionString;


        public IEnumerable<Klant> GeefKlanten() {
            List<Klant> klanten = new List<Klant>();
            string query = "SELECT k.id AS id, k.naam AS naam, a.id AS adresid, a.adres AS adres FROM Klant k INNER JOIN Adres a ON k.adres_id = a.id;";
            SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            try {
                connection.Open();

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    klanten.Add(ParseKlant(reader));
                }

                reader.Close();

                return klanten.AsEnumerable();
            }
            catch {
                // TODO create execption
                throw new Exception();
            }
            finally {
                connection.Close();
            }
        }

        private static Klant ParseKlant(SqlDataReader reader) {
            int klantnummer = (int)reader["id"];
            string naam = (string)reader["naam"];
            int adresid = (int)reader["adresid"];
            string adres = (string)reader["adres"];

            return new Klant(klantnummer, naam, new Adres(adres));
        }
    }
}
