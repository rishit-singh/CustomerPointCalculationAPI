using System;

namespace CustomerPointCalculationAPI
{
    public class UserManager
    {
        public static User CreateUser(string name) => null;
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
