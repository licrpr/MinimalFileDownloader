using System;

namespace MinimalFileDownloader.Common.SSH
{
    public static class ParsingExtensions
    {
        public static int? TryParseInt(this string text)
        {
            int result;
            if (Int32.TryParse(text, out result))
                return result;

            return null;
        }

        public static string TrimQuotes(this string text)
        {
            return text.Trim().Trim('"').Trim();
        }

        public static string[] SplitLines(this string text)
        {
            return text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        public static string Escape(this string text)
        {
            return text
                .Replace("\"", "\\\"");
        }
    }
}