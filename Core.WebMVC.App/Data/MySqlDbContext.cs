using System.Data;

using MySqlConnector;

namespace DMMS.WebMVC.App.Data
{
    public class MySqlDbContext
    {
        private readonly string _connectionString;

        public MySqlDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // ✅ EXECUTE NON-QUERY (INSERT, UPDATE, DELETE)
        public async Task<int> ExecuteNonQueryAsync(string query, List<MySqlParameter>? parameters = null)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            return await command.ExecuteNonQueryAsync();
        }

        // ✅ EXECUTE SCALAR (COUNT, SUM, SINGLE VALUE)
        public async Task<object?> ExecuteScalarAsync(string query, List<MySqlParameter>? parameters = null)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            return await command.ExecuteScalarAsync();
        }

        // ✅ EXECUTE READER (MULTIPLE ROWS)
        public async Task<DataTable> ExecuteQueryAsync(string query, List<MySqlParameter>? parameters = null)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            using var reader = await command.ExecuteReaderAsync();

            var table = new DataTable();
            table.Load(reader);

            return table;
        }

        // ✅ GENERIC LIST MAPPER (BEST PRACTICE)
        public async Task<List<T>> ExecuteQueryAsync<T>(string query, Func<MySqlDataReader, T> map, List<MySqlParameter>? parameters = null)
        {
            var result = new List<T>();

            using var connection = GetConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(map(reader));
            }

            return result;
        }

        // ✅ TRANSACTION SUPPORT
        public async Task<bool> ExecuteTransactionAsync(List<(string query, List<MySqlParameter> parameters)> commands)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                foreach (var cmd in commands)
                {
                    using var command = new MySqlCommand(cmd.query, connection, (MySqlTransaction)transaction);

                    if (cmd.parameters != null)
                        command.Parameters.AddRange(cmd.parameters.ToArray());

                    await command.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // ✅ INSERT / UPDATE / DELETE
        public async Task<int> ExecuteNonQueryAsync(string query)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);
            return await command.ExecuteNonQueryAsync(); // returns affected rows
        }

        // ✅ SCALAR (returns first column of first row)
        public async Task<object?> ExecuteScalarAsync(string query)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);
            return await command.ExecuteScalarAsync();
        }

        // ✅ SELECT (DataTable)
        public async Task<DataTable> ExecuteQueryAsync(string query)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            var table = new DataTable();
            table.Load(reader);

            return table;
        }
    }
}
