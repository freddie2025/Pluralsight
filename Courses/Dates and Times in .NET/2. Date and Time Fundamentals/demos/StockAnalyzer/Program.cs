using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace StockAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Parsing a date and time given a specific format
            var date = "9/10/2019 10:00:00 PM";

            var parsedDate = DateTimeOffset.ParseExact(date,
                "M/d/yyyy h:mm:ss tt",
                CultureInfo.InvariantCulture);

            Console.WriteLine(parsedDate);
            #endregion

            #region Formatting a date and time as ISO 8601
            Console.WriteLine(parsedDate.ToString("o"));
            #endregion

            #region Finding time zones for a given offset
            var now = DateTimeOffset.Now;

            foreach (var timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                if (timeZone.GetUtcOffset(now) == now.Offset)
                {
                    Console.WriteLine(timeZone);
                }
            }

            #endregion
        }
    }
}
