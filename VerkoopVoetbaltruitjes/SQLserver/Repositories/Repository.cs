using Domein;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLserver.Repositories {
    public class Repository {
        public static object VoerDBActieUit(Func<IDbCommand, object> func) {
            IDbConnection connection = new SqlConnection(Config.ConnectionString);
            using (IDbCommand command = connection.CreateCommand()) {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    object resultaat = func.Invoke(command);

                    transaction.Commit();

                    return resultaat;
                }
                catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception("VoegKlantToe: Er trad een fout op bij het raadplegen van de database.", ex);
                } finally {
                    connection.Close();
                }
            }
        }

        public static void VoerDBActieUit(Action<IDbCommand> action) {
            IDbConnection connection = new SqlConnection(Config.ConnectionString);
            using (IDbCommand command = connection.CreateCommand()) {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    action.Invoke(command);

                    transaction.Commit();
                }
                catch (Exception ex) {
                    transaction.Rollback();
                    throw new Exception("VoegKlantToe: Er trad een fout op bij het raadplegen van de database.", ex);
                } finally {
                    connection.Close();
                }
            }
        }
    }
}
