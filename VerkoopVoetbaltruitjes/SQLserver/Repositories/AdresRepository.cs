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
    public class AdresRepository : IAdresRepository {
        string _connectionString = Config.ConnectionString;

        public IEnumerable<Adres> GeefAdressen() {
            List<Adres> adressen = new List<Adres>();
            string query = "SELECT * FROM Adres;";
            SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            try {
                connection.Open();

                command.CommandText = query;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    adressen.Add(ParseAdres(reader));
                }

                reader.Close();

                return adressen.AsEnumerable();
            }
            catch (Exception ex){
                // TODO create execption
                throw new Exception("", ex);
            }
            finally {
                connection.Close();
            }
        }

        private static Adres ParseAdres(SqlDataReader reader) {
            string adres = (string)reader["adres"];

            return new Adres(adres);
        }
    }
}
