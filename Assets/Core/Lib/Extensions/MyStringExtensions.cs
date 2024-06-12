namespace Lib
{
    public static class MyStringExtensions
    {
        public static string FormatAddCharBeforeCapitalLetters(this string value, bool includeNumbers = false,
            char separator = ' ')
        {
            return includeNumbers
                ? System.Text.RegularExpressions.Regex.Replace(value, "(?!^)[A-Z0-9]", $"{separator}$0")
                : System.Text.RegularExpressions.Regex.Replace(value, "(?!^)[A-Z]", $"{separator}$0");
        }
    }
}