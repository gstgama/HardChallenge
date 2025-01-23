using SmartVault.DataAccess.Interfaces;
using System.Collections.Generic;

namespace SmartVault.DataAccess.Services
{
    public class SQLiteDatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public SQLiteDatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<string> GetFilePaths(string query, Dictionary<string, object> parameters = null)
        {
            var filePaths = new List<string>();

            using (var conn = new System.Data.SQLite.SQLiteConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new System.Data.SQLite.SQLiteCommand(query, conn))
                {
                    if (parameters is not null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filePaths.Add(reader["FilePath"].ToString());
                        }
                    }
                }
            }

            return filePaths;
        }
    }
}