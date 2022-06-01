using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using CustomerPointCalculationAPI.Logs;

namespace CustomerPointCalculationAPI.Database
{
    /// <summary>
    /// Stores configuration for the databse connection to the PostGRES server.
    /// </summary>
    public struct DatabaseConfig
    {
        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DatabaseName { get; set; }

        public string GetConnectionString()
        {
            return $"Host={this.Host};Username={this.Username};Password={this.Password};DatabaseName={this.DatabaseName}";
        }

        public DatabaseConfig(string host, string username, string password, string databaseName)
        {
            this.Host = host;
            this.Username = username;
            this.Password = password;
            this.DatabaseName = databaseName;
        }
    }

    public class Database
    {
        public static string DefaultDatabaseConfigFile = "DatabaseConfig.json";

        public static DatabaseConfig DefaultDatabaseConfig = new DatabaseConfig();

        public static bool LoadDatabaseConfig()
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
    }
}
