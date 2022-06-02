using System;

using CustomerPointCalculationAPI;
using CustomerPointCalculationAPI.Logs;


namespace CustomerPointCalculationAPI.Tests
{
    public class UserCreationTest : ITest
    {
        public User AddTestUser(string name)
        {
            User user = null;

            try
            {
                if (name == null)
                    throw new Exception("Null username provided.");

                user = UserManager.CreateUser(name);

                Database.ExecuteQuery($"INSERT INTO Users VALUES('{user.ID}', '{user.Name}');");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return user;
        }
        
        public void Run()
        {
            Random rand = new Random();

            this.AddTestUser($"foobar_{rand.Next(0, 1000)}");
        }
    }
}
