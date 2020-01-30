using System;

namespace UnixTimestamp
{
    class Program
    {
        static void Main(string[] args)
        {
            var timestamp = 1562335678;

            var unixDateStart = new DateTime(1970, 01, 01, 00, 00, 00, DateTimeKind.Utc);
            var result = unixDateStart.AddSeconds(timestamp);

            Console.WriteLine(new DateTimeOffset(result).ToUnixTimeSeconds());

            Console.WriteLine(result);
        }
    }
}
