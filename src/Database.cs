using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            // try
            // {
                Database.DefaultSqlConnection = new NpgsqlConnection(Database.DefaultDatabaseConfig.GetConnectionString());

                Database.DefaultSqlConnection.Open();
            // }
            // catch (Exception e)
            // {
            //     Logger.Log(e.Message, true);

            //     return false;
            // }

           return true;
        }

        /// <summary>
        /// Loads the Database server configuration from the default config file into Database.DefaultDatabaseConfig object.
        /// </summary>
        /// <returns> Execution status. </returns>
        protected static bool LoadDatabaseConfig()
        {
            string configJsonString = null;

            // try
            // {
                if ((configJsonString = FileIO.ReadFile(Database.DefaultDatabaseConfigFile)) == null)
                    throw new Exception("Failed to load DatabaseConfig, the configuration file is either empty or doesnt exist.");

                Database.DefaultDatabaseConfig = JsonConvert.DeserializeObject<DatabaseConfig>(configJsonString);
            // }
            // catch (Exception e)
            // {
            //     Logger.Log(e.Message, true);

            //     return false;
            // }

            return true;
        }

        protected static Record[] GetRecordsFromReader(NpgsqlDataReader reader, string[] fields)
        {
            List<Record> recordStack = new List<Record>();

            Record tempRecord = new Record();

            try
            {
                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;

                    tempRecord = new Record(new string[fieldCount], new object[fieldCount]);

                    for (int x = 0; x < fieldCount; x++)
                    {
                        tempRecord.Keys[x] = fields[x];
                        tempRecord.Values[x] = (reader.GetDataTypeName(x) == "integer") ? reader.GetInt32(x).ToString() : reader.GetString(x);
                    }

                    recordStack.Add(tempRecord);
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return recordStack.ToArray();
        }

        protected static string[] GetTableFieldNames(string tableName)
        {
            List<string> fields = new List<string>();

            NpgsqlCommand command = null;

            NpgsqlDataReader reader = null;

            try
            {
                Console.WriteLine($"SELECT column_name FROM INFORMATION_SCHEMA. COLUMNS WHERE TABLE_NAME = '{tableName}';");
                command = new NpgsqlCommand($"SELECT column_name FROM INFORMATION_SCHEMA. COLUMNS WHERE TABLE_NAME = '{tableName}';", Database.DefaultSqlConnection);

                reader = command.ExecuteReader();

                Console.WriteLine($"Rows: {reader.Rows}");

                while(reader.Read())
                    switch (reader.GetDataTypeName(0))
                    {
                        case "integer":
                            fields.Add(reader.GetInt32(0).ToString());
                            break;

                        default:
                            fields.Add(reader.GetString(0));
                            break;
                    }

                reader.Close();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return fields.ToArray();
        }

        public static bool ExecuteQuery(string query)
        {
            NpgsqlCommand command = null;

            try
            {
                command = new NpgsqlCommand(query, Database.DefaultSqlConnection);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);

                return false;
            }

            return true;
        }

        public static int GetRecordCount(string tableName)
        {
            int count = 0;

            NpgsqlCommand command = null;

            NpgsqlDataReader reader = null;

            try
            {
                command = new NpgsqlCommand($"SELECT * FROM {tableName}", Database.DefaultSqlConnection);

                reader = command.ExecuteReader();

                for (; reader.Read(); count++);

                reader.Close();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);

                return count;
            }

            return count;
        }

        public static Record[] FetchQueryData(string query, string tableName)
        {
            NpgsqlCommand command = null;

            Record[] fetchedRecords = null;

            NpgsqlDataReader reader = null;

            try
            {
                command = new NpgsqlCommand(query, Database.DefaultSqlConnection);

                string[] fields = Database.GetTableFieldNames(tableName);

                reader = command.ExecuteReader();

                fetchedRecords = Database.GetRecordsFromReader(reader, fields);

                reader.Close();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return fetchedRecords;
        }
    }
}
