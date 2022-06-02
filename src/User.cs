using System;
using Cryptography;

using CustomerPointCalculationAPI.Logs;

namespace CustomerPointCalculationAPI
{
    public class UserManager
    {
        public static User CreateUser(string name)
        {
            User user = null;

            try
            {
                user = new User(null, null);

                if (name == null)
                    throw new Exception("Null username provided.");

                user.ID = Hashing.GetSHA256($"{name}_{Database.GetRecordCount("Users")}");
                user.Name = name;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return user;
        }

        public static bool PushUser(User user)
        {
            try
            {
                if (user.ID == null)
                    throw new Exception("Null User provided.");

                Database.ExecuteQuery($"INSERT INTO Users VALUES('{user.ID}', '{user.Name}');");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);

                return false;
            }

            return true;
        }

        public static User[] GetAllUsers()
        {
            User[] users = null;

            Record[] records = null;

            try
            {
                records = Database.FetchQueryData("SELECT * FROM users;", "users");

                int size = records.Length;

                users = new User[size];

                for (int x = 0; x < size; x++)
                    users[x] = new User(records[x]);
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return users;
        }
    }

    public class User
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public User(string id, string name)
        {
        }

        public User(Record record)
        {
            this.ID = (string)record.Values[0];
            this.Name = (string)record.Values[1];
        }
    }
}
