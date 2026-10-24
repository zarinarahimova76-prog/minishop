using Npgsql;

namespace Infrastructure.Data;

public class Database
{
    private readonly string _connectionString = "Host=localhost;Port=5432;Database=minishop;Username=postgres;Password=1234";

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}