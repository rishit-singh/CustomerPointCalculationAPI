using System;

using CustomerPointCalculationAPI;
using CustomerPointCalculationAPI.Logs;


namespace CustomerPointCalculationAPI.Tests
{
    public class TransactionCreationTest : ITest
    {
        public Transaction AddTestTransaction(string userID, int amount)
        {
            Transaction transaction = null;

            try
            {
                if (userID == null)
                    throw new Exception("Null user ID provided.");

                transaction = TransactionManager.CreateTransaction(userID, amount, TestTools.GetRandomDateTime());

                Database.ExecuteQuery($"INSERT INTO Transactions VALUES('{transaction.ID}', '{transaction.UserID}', {transaction.Amount}, '{transaction.TransactionDateTime}');");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return transaction;
        }

        public void Run()
        {
            User[] users = UserManager.GetAllUsers();

            Random rand = new Random();

            this.AddTestTransaction(users[rand.Next(0, users.Length)].ID, rand.Next(10, 10000));
        }
    }
}
