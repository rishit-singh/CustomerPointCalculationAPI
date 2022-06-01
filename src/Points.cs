using System;

namespace CustomerPointCalculationAPI
{
   public class PointCalculator
   {
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

      public static int GetTotalTransactionPoints(User user) => 0;
   }
}
