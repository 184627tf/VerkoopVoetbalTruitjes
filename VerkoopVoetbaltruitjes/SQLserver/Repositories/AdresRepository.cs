using Domein;
using Domein.Interfaces;
using Domein.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
            int? id = (int)reader["id"];
            string adres = (string)reader["adres"];

            return new Adres(id, adres);
        }

        public void UpdateAdres(Adres adres) {
            string update = "UPDATE Adres SET adres=@adres WHERE id=@id;";
            IDbConnection connection = new SqlConnection(_connectionString);
            using (IDbCommand command = connection.CreateCommand()) {
                connection.Open();

                try {
                    command.CommandText = update;
                    command.Parameters.Add(new SqlParameter("id", adres.Id));
                    command.Parameters.Add(new SqlParameter("adres", adres.Adresnaam));

                    command.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    throw new Exception("UpdateAdres: Er trad een fout op bij het raadplegen van de database.", ex);
                }
                finally {
                    connection.Close();
                }
            }
        }

        public void VerwijderAdres(int id) {
            string delete = "DELETE FROM Adres WHERE id=@id";
            IDbConnection connection = new SqlConnection(_connectionString);
            using (IDbCommand command = connection.CreateCommand()) {
                connection.Open();

                try {
                    command.CommandText = delete;
                    command.Parameters.Add(new SqlParameter("id", id));

                    command.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    throw new Exception("VerwijderAdres: Er trad een fout op bij het raadplegen van de database.", ex);
                }
                finally {
                    connection.Close();
                }
            }
        }

        public int VoegAdresToe(Adres adres) {
            string insert = "INSERT INTO Adres (adres) OUTPUT INSERTED.id VALUES (@adres);";
            IDbConnection connection = new SqlConnection(_connectionString);
            using (IDbCommand command = connection.CreateCommand()) {
                connection.Open();

                try {
                    command.CommandText = insert;
                    command.Parameters.Add(new SqlParameter("adres", adres.Adresnaam));
                    int id = (int)command.ExecuteScalar();

                    return id;
                }
                catch (Exception ex) {
                    throw new Exception("VoegAdresToe: Er trad een fout op bij het raadplegen van de database.", ex);
                }
                finally {
                    connection.Close();
                }
            }
        }
    }
}
