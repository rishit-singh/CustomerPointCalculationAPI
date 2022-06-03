using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

using CustomerPointCalculationAPI.Logs;

using Npgsql;

namespace CustomerPointCalculationAPI
{
    public class UserPoints
    {
        public string UserID { get; set; }

        public Hashtable MonthlyPoints { get; set; }

        public int TotalPoints;

        public string GetJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        private void InitMonthyPointsHash()
        {
            int size = PointCalculator.Months.Length;

            this.MonthlyPoints = new Hashtable();

            for (int x = 0; x < size; x++)
                this.MonthlyPoints.Add(PointCalculator.Months[x], 0);
        }

        public int GetTotalPoints()
        {
            int total = 0;

            ICollection values = this.MonthlyPoints.Values;

            foreach (object value in values)
                total += (int)value;

            return (this.TotalPoints = total);
        }

        public UserPoints(string userID)
        {
            this.UserID = userID;

            this.InitMonthyPointsHash();
        }
    }
    
    public class PointCalculator
    {
        public static string[] Months = new string[] {
            "January",
            "Febuary",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };

        /// <summary>
        /// Calculates the points to be granted to the user for the provided Transaction.
        /// </summary>
        /// <param name="transaction"> Made transaction. </param>
        /// <returns> Calculated points. </returns>
        public static int GetTransactionPoints(Transaction transaction)
        {
            int points = 0;

            if (transaction.Amount > 50)
                points += transaction.Amount - 50;

            if (transaction.Amount > 100)
                points += transaction.Amount - 100;

            return points;
        }

        public static int GetTotalTransactionPoints(User user)
        {
            Transaction[] transactions = null;

            int totalPoints = 0;

            try
            {
               if ((transactions = TransactionManager.GetTransactionsByUser(user)).Length == 0)
                   throw new Exception($"User {user.ID} has made no transactions.");

               int size = transactions.Length;

               for (int x = 0; x < size; x++)
                   totalPoints += PointCalculator.GetTransactionPoints(transactions[x]);
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return totalPoints;
        }

        public static UserPoints GetUserPoints(User user)
        {
            UserPoints userPoints = null;

            Transaction[] transactions = null;

            try
            {
                if ((transactions = TransactionManager.GetTransactionsByUser(user)).Length == 0)
                    throw new Exception($"No transactions made by User {user.ID} were found.");

                userPoints = new UserPoints(user.ID);

                int size = transactions.Length;

                string key = null;

                for (int x = 0; x < size; x++)
                {
                    key = PointCalculator.Months[transactions[x].TransactionDateTime.Month];

                    Console.WriteLine(key);

                    userPoints.MonthlyPoints[key] = PointCalculator.GetTransactionPoints(transactions[x]);
                }

                userPoints.GetTotalPoints();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, true);
            }

            return userPoints;
        }
    }
}
