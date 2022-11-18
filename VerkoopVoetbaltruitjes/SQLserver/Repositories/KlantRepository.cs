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
    public class KlantRepository : IKlantRepository {
        string _connectionString = Config.ConnectionString;

        public int VoegKlantToe(Klant klant) {
            IDbConnection connection = new SqlConnection(_connectionString);
            using (IDbCommand command = connection.CreateCommand()) {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    // See if adress exists -> if not add it
                    // TODO: DB make adres UNIQUE in Adres (+ Cascading DELETE?)
                    command.CommandText = "SELECT COUNT(*) FROM Adres WHERE adres=@adresnaam;";
                    IDataParameter adresParameter = new SqlParameter("adresnaam", klant.Adres.Adresnaam);
                    command.Parameters.Add(adresParameter);

                    int amount = (int)command.ExecuteScalar();

                    if (amount <= 0) {
                        command.CommandText = "INSERT INTO Adres (adres) OUTPUT INSERTED.id VALUES (@adres);";
                    } else {
                        command.CommandText = "SELECT TOP 1 id FROM Adres WHERE adres=@adres;";
                    }
                    command.Parameters.Add(new SqlParameter("adres", klant.Adres.Adresnaam));
                    int adresId = (int)command.ExecuteScalar();

                    // Insert klant
                    command.CommandText = "INSERT INTO Klant (naam, adres_id) OUTPUT INSERTED.id VALUES (@naam, @adres_id);";
                    command.Parameters.Add(new SqlParameter("naam", klant.Naam));
                    command.Parameters.Add(new SqlParameter("adres_id", adresId));
                    int klantId = (int)command.ExecuteScalar();
                    
                    transaction.Commit();

                    return klantId;
                }
                catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception("VoegKlantToe: Er trad een fout op bij het raadplegen van de database.", ex);
                } finally {
                    connection.Close();
                }
            }
        }

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
        
        public void VerwijderKlant(Klant klant) {
            string delete = "DELETE FROM Klant WHERE id=@id;";
            IDbConnection connection = new SqlConnection(_connectionString);
            using (IDbCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.CommandText = delete;
                    IDataParameter idParameter = new SqlParameter("id", klant.Id);
                    command.Parameters.Add(idParameter);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    throw new Exception("VoegKlantToe: Er trad een fout op bij het raadplegen van de database.", ex);
                }
                finally {
                    connection.Close();
                }
            }
        }

        public bool Exists(Klant klant) {
            string query = "SELECT COUNT(*) FROM Klant WHERE id=@id;";
            IDbConnection connection = new SqlConnection(_connectionString);
            using (IDbCommand command = connection.CreateCommand()) {
                connection.Open();
                try {
                    command.CommandText = query;
                    IDataParameter idParameter = new SqlParameter("id", klant.Id);
                    command.Parameters.Add(idParameter);

                    int amount = (int)command.ExecuteScalar();

                    return amount > 0;
                }
                catch (Exception ex) {
                    throw new Exception("VoegKlantToe: Er trad een fout op bij het raadplegen van de database.", ex);
                }
                finally {
                    connection.Close();
                }
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
