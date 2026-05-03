using Microsoft.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data;


namespace DMMS.WebMVC.App.Helper
{
    public class DatabaseHelper
    {
        public static async Task<DataTable> RunMySqlQueryAsync(string connectionString, string query)
        {
            var dt = new DataTable();

            using var conn = new MySqlConnection(connectionString);
            using var cmd = new MySqlCommand(query, conn);

            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            dt.Load(reader);

            return dt;
        }
    }
}
