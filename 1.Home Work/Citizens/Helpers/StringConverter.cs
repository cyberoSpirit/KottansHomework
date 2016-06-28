using System.Globalization;

namespace Citizens.Helpers
{
    public static class StringConverter
    {
        public static string ToTitleCase(this string value)
        {
            return new CultureInfo("en").TextInfo.ToTitleCase(value);
            //return String.Join(" ", value.Split(' ')
            //    .Select(i => i.Substring(0, 1).ToUpper() + i.Substring(1).ToLower()).ToArray());
        }
    }
}
