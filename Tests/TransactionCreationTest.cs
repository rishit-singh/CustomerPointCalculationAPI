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
            Random rand = new Random();

            this.AddTestTransaction($"foobar_{rand.Next(0, 1000)}", rand.Next(10, 10000));
        }
    }
}
