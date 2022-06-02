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
    }

    public class User
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public User(string id, string name)
        {
        }
    }
}
