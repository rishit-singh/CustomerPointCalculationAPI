using System;
using CustomerPointCalculationAPI.Logs;


namespace CustomerPointCalculationAPI.Tests
{
    public class PointCalculationTest : ITest
    {
        public User GetRandomUser()
        {
            User user = null;

            Random rand = new Random();

            try
            {
                User[] users = UserManager.GetAllUsers();

                user = users[rand.Next(0, users.Length)];
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return user;
        }

        public void Run()
        {
            UserPoints userPoints = null;

            try
            {
                Console.WriteLine((userPoints = PointCalculator.GetUserPoints(this.GetRandomUser())).GetJsonString());
            }
            catch (NullReferenceException){}
        }
    }
}
