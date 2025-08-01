using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

public static class DatabaseHelper
{
    private static readonly string _connectionString;

    static DatabaseHelper()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();
        _connectionString = config.GetConnectionString("MyConnection");
    }

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }

    public static MySqlDataReader ExecuteReader(string query, MySqlParameter[] parameters = null)
    {
        var conn = GetConnection();
        conn.Open();
        var cmd = new MySqlCommand(query, conn);
        if (parameters != null)
            cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public static int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
    {
        using var conn = GetConnection();
        conn.Open();
        using var cmd = new MySqlCommand(query, conn);
        if (parameters != null)
            cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteNonQuery();
    }

    public static object ExecuteScalar(string query, MySqlParameter[] parameters = null)
    {
        using var conn = GetConnection();
        conn.Open();
        using var cmd = new MySqlCommand(query, conn);
        if (parameters != null)
            cmd.Parameters.AddRange(parameters);
        return cmd.ExecuteScalar();
    }
    public static MySqlParameter[] CreateMySqlParameters(Dictionary<string, object> parameterDict)
    {
        return parameterDict
                    .Select(p => new MySqlParameter(p.Key, p.Value ?? DBNull.Value))
                    .ToArray();
    }
}
