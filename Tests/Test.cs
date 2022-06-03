using System;

namespace CustomerPointCalculationAPI.Tests
{
    public interface ITest
    {
        void Run();
    }

    public class TestTools
    {
        public static DateTime GetRandomDateTime()
        {
            Random rand = new Random();

            int month = rand.Next(1, 12),
                day = 1;

            if ((month % 2) == 0) // alternate
            {
                if (month == 2)
                    if ((DateTime.Now.Year % 4) == 0) // leap year check
                        day =rand.Next(1, 29);
                    else
                        rand.Next(1, 28);
            }
            else
                day = rand.Next(1, 31);

            return new DateTime(DateTime.Now.Year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }
    }
}
