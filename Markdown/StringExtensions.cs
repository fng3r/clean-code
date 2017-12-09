using System.Text;

namespace Markdown
{
    public static class StringExtensions
    {
        public static string Unescape(this string str, char escapeCharacter = '\\') => str.UnescapeSubstring(0, str.Length, escapeCharacter);

        public static string UnescapeSubstring(this string str, int startIndex = 0, char escapeCharacter = '\\') => str.UnescapeSubstring(startIndex, str.Length);

        public static string UnescapeSubstring(this string str, int startIndex, int length, char escapeCharacter = '\\')
        {
            var result = new StringBuilder();
            var i = startIndex;
            while (i < startIndex + length)
            {
                if (str[i] == escapeCharacter && i < startIndex + length) result.Append(str[++i]);
                else result.Append(str[i]);

                i++;
            }
            return result.ToString();
        }

        public static bool SubstringMatch(this string str, int startIndex, string pattern)
        {
            if (str.Length - startIndex < pattern.Length) return false;

            for (var i = 0; i < pattern.Length; i++)
                if (str[startIndex + i] != pattern[i]) return false;

            return true;
        }
    }
}