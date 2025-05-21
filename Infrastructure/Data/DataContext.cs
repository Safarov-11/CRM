using System.Data;
using System.IO.Pipelines;
using Npgsql;

namespace Infrastructure.Data;

public class DataContext
{
    private const string connectionString = "Host = localhost; Database = CRM;User id = postgres; password = sr000080864";
    public Task<NpgsqlConnection> GetDbConnectionAsync(){
        return Task.FromResult(new NpgsqlConnection(connectionString));
    }
}
