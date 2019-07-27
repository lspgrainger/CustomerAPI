using System.IO;
using Dapper;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace Api.Customer.Repository
{
    using Customer = Api.Customer.Domain.Customer;

    public class CustomerSQLiteDatabase : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public CustomerSQLiteDatabase(string sqliteDbConnectionString)
        {
            _connectionString = sqliteDbConnectionString;
            InitialiseCustomerDatabase(sqliteDbConnectionString);
        }

        private void InitialiseCustomerDatabase(string sqliteDbConnectionString)
        {
            //Data Source=c:\customerDb.sqlite;Version=3;
            var matches = Regex.Match(sqliteDbConnectionString, @"Data Source=(?<sqlLiteFile>.*?);Version=3;");
            var customerDbFile = matches.Groups["sqlLiteFile"].Value;

            if (!File.Exists(customerDbFile))
            {
                SQLiteConnection.CreateFile(customerDbFile);
            }
            using (var connection = CreateConnection())
            {
                connection.Execute($@"
        CREATE TABLE IF NOT EXISTS [{nameof(Customer)}] (
            [{nameof(Customer.CustomerId)}] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            [{nameof(Customer.EmailAddress)}] NVARCHAR(128) NOT NULL,
            [{nameof(Customer.Forename)}] NVARCHAR(128) NOT NULL,
            [{nameof(Customer.Surname)}] NVARCHAR(128) NOT NULL,
            [{nameof(Customer.Password)}] NVARCHAR(128) NOT NULL,
            [DateCreated] TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        )");
            }
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            return connection;
        }
    }
}