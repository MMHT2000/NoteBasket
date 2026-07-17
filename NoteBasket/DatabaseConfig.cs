using System;
using System.Configuration;
using System.Data.SqlClient;

namespace NoteBasket
{
    internal static class DatabaseConfig
    {
        private const string ConnectionStringName = "NoteBasketDB";

        internal static string ConnectionString
        {
            get
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[ConnectionStringName];

                if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
                {
                    throw new InvalidOperationException($"Missing connection string '{ConnectionStringName}' in App.config.");
                }

                return settings.ConnectionString;
            }
        }

        internal static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
