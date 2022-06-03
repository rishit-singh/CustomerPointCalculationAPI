using System;
using System.Collections;
using System.Collections.Generic;

using CustomerPointCalculationAPI.Logs;

using Cryptography;

namespace CustomerPointCalculationAPI
{
    public class TransactionManager
    {
        /// <summary>
        /// Pushes the provieded transaction to the database after validation.
        /// </summary>
        /// <param name="transcation"> Transaction instance. </param>
        /// <returns> Execution status. </returns>
        public static bool PushTransaction(Transaction transaction)
        {
            try
            {
                if (transaction.IsEmpty())
                    throw new Exception("Empty Transaction object provided.");

                Database.ExecuteQuery($"INSERT INTO transations VALUES('{transaction.ID}', '{transaction.UserID}', {transaction.Amount}, '{transaction.TransactionDateTime.ToString()}');");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);

                return false;
            }

            return true;
        }

        public static Transaction[] GetTransactionsByUser(User user)
        {
            List<Transaction>  transactions = new List<Transaction>();

            Record[] records = null;

            try
            {
                records = Database.FetchQueryData($"SELECT * FROM transactions WHERE userid='{user.ID}';", "transactions");

                int size = records.Length;

                Console.WriteLine($"Record Size: {size}");

                for (int x = 0; x < size; x++)
                    transactions.Add(new Transaction(records[x]));
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return transactions.ToArray();
        }

        public static int GetTotalTransactionCount()
        {
            int count = 0;

            try
            {
                count = Database.FetchQueryData("SELECT * FROM transactions;", "transaction").Length;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return count;
        }

        public static Transaction CreateTransaction(string userID, int amount, DateTime time)
        {
            Transaction transaction = null;

            try
            {
                transaction = new Transaction(userID, amount, time);
                transaction.ID = Hashing.GetSHA256($"{userID}_{amount}_{time.ToString()}_{TransactionManager.GetTotalTransactionCount()}");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return transaction;
        }
   }

    public class Transaction
    {
        public string ID { get; set; }

        public string UserID { get; set; }

        public int Amount { get; set; }

        public DateTime TransactionDateTime { get; set; }

        public bool IsEmpty()
        {
            return (this.ID == null &&
            this.UserID == null &&
            this.Amount == 0);
        }

        public Transaction(string id, int amount, DateTime dateTime)
        {
            this.UserID = id;
            this.Amount = amount;
            this.TransactionDateTime = dateTime;
        }

        public Transaction(Record record)
        {
            this.ID = (string)record.Values[0];
            this.UserID = (string)record.Values[1];
            this.Amount = (int)record.Values[2];
            this.TransactionDateTime = Convert.ToDateTime((string)record.Values[3]);
        }
    }
}
