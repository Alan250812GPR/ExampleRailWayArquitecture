using MySql.Data.MySqlClient;

public class DatabaseManager
{
    private readonly string connectionString;

    public DatabaseManager(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<Result<MySqlConnection>> OpenConnectionAsync()
    {
        try
        {
            var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            return new Result<MySqlConnection>(connection);
        }
        catch (MySqlException ex)
        {
            return new Result<MySqlConnection>(ex.Message);
        }
    }

    public async Task<Result<MySqlDataReader>> ExecuteReaderAsync(MySqlConnection connection, string sql)
    {
        try
        {
            var command = new MySqlCommand(sql, connection);
            var reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            return new Result<MySqlDataReader>(reader);
        }
        catch (Exception ex)
        {
            return new Result<MySqlDataReader>(ex.Message);
        }
    }
}