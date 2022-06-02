using System;
using System.Collections;
using System.Collections.Generic;
using Npgsql;
using Newtonsoft.Json;
using CustomerPointCalculationAPI.Logs;

namespace CustomerPointCalculationAPI
{
    /// <summary>
    /// Stores configuration for the databse connection to the PostGRES server.
    /// </summary>
    public struct DatabaseConfig
    {
        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Database { get; set; }

        public bool IsEmpty()
        {
            return (this.Host == null &&
                this.Username == null &&
                this.Password == null &&
                this.Database == null);
        }

        public string GetConnectionString()
        {
            return $"Host={this.Host};Username={this.Username};Password={this.Password};Database={this.Database}";
        }

        public DatabaseConfig(string host, string username, string password, string database)
        {
            this.Host = host;
            this.Username = username;
            this.Password = password;
            this.Database = database;
        }
    }

    /// <summary>
    /// Stores a database table record.
    /// </summary>
    public struct Record
    {
        public string[] Keys;

        public object[] Values;

        public Record(string[] keys, object[] values)
        {
            this.Keys = keys;

            this.Values = values;
        }
    }

    public class Database
    {
        public static string DefaultDatabaseConfigFile = "DatabaseConfig.json";

        public static DatabaseConfig DefaultDatabaseConfig = new DatabaseConfig();

        public static NpgsqlConnection DefaultSqlConnection = new NpgsqlConnection();

        /// <summary>
        /// Configures and connects to the database server.
        /// </summary>
        /// <returns> Execution status. </returns>
        public static bool Connect()
        {
            if (Database.DefaultDatabaseConfig.IsEmpty())
                Database.LoadDatabaseConfig();

            try
            {
                Database.DefaultSqlConnection = new NpgsqlConnection(Database.DefaultDatabaseConfig.GetConnectionString());
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);

                return false;
            }

           return true;
        }

        /// <summary>
        /// Loads the Database server configuration from the default config file into Database.DefaultDatabaseConfig object.
        /// </summary>
        /// <returns> Execution status. </returns>
        protected static bool LoadDatabaseConfig()
        {
            string configJsonString = null;

            try
            {
                if ((configJsonString = FileIO.ReadFile(Database.DefaultDatabaseConfigFile)) == null)
                    throw new Exception("Failed to load DatabaseConfig, the configuration file is either empty or doesnt exist.");

                Database.DefaultDatabaseConfig = JsonConvert.DeserializeObject<DatabaseConfig>(configJsonString);
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);

                return false;
            }

            return true;
        }

        public static Record[] FetchQueryData()
        {
            return new Record[0];
        }
    }
}
