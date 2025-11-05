namespace XrmToolsHelloWorld
{
    /// <summary>
    /// Provides functionality to format integers as ordinal numbers.
    /// </summary>
    /// <remarks>An ordinal number is a number that indicates position or order in a sequence, such as "1st",
    /// "2nd", "3rd", etc. This service formats integers into their corresponding ordinal representation as a
    /// string.</remarks>
    public interface IOrdinalFormatterService
    {
        string FormatOrdinal(int number);
    }

    /// <inheritdoc/>
    public class OrdinalFormatterService : IOrdinalFormatterService
    {
        public string FormatOrdinal(int number)
        {
            // Handle negative and zero numbers gracefully
            if (number <= 0)
                return number.ToString();

            int lastTwoDigits = number % 100;
            int lastDigit = number % 10;

            string suffix;

            // Special case for 11, 12, 13
            if (lastTwoDigits >= 11 && lastTwoDigits <= 13)
            {
                suffix = "th";
            }
            else
            {
                switch (lastDigit)
                {
                    case 1:
                        suffix = "st";
                        break;
                    case 2:
                        suffix = "nd";
                        break;
                    case 3:
                        suffix = "rd";
                        break;
                    default:
                        suffix = "th";
                        break;
                }
            }

            return $"{number}{suffix}";
        }
    }
}
