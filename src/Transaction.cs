using System;

namespace CustomerPointCalculationAPI
{
    public class Transaction
    {
        public string ID { get; set; }

        public string UserID { get; set; }

        public int Amount { get; set; }

        public Transaction(string id, int amount)
        {
            this.UserID = id;
            this.Amount = amount;
        }
    }
}
