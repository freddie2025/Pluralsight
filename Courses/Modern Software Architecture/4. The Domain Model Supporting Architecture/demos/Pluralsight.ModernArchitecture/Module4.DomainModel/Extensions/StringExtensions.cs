using System;
using System.Linq;

namespace Module4.DomainModel.Extensions
{
    public static class StringExtensions
    {
        public static Boolean ContainsAny(this String theString, params String[] tokens)
        {
            return tokens.Any(theString.Contains);
        }

        public static Boolean EqualsAny(this String theString, params String[] tokens)
        {
            return tokens.Any(token => theString.Equals(token, StringComparison.InvariantCultureIgnoreCase));
        }

        public static Uri ToUri(this String url, UriKind kind = UriKind.RelativeOrAbsolute)
        {
            return new Uri(url, kind);
        }

        public static DateTime? ToDate(this String theString, DateTime? defaultDate = null)
        {
            DateTime date;
            var success = DateTime.TryParse(theString, out date);
            return success ? date : defaultDate;
        }

        public static Boolean ToBool(this String theString)
        {
            Boolean value;
            var success = Boolean.TryParse(theString, out value);
            return success && value;
        }

        public static Int32 ToInt(this String theString, Int32 defaultValue = 0)
        {
            Int32 number;
            var success = Int32.TryParse(theString, out number);
            return success ? number : defaultValue;
        }

        public static bool IsNullOrWhitespace(this String theString)
        {
            return String.IsNullOrWhiteSpace(theString);
        }

        public static bool IsAlphaNumeric(this String theString)
        {
            return String.IsNullOrWhiteSpace(theString);
        }
    }
}
