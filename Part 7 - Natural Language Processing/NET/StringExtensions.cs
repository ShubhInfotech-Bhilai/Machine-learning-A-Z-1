using System.Text.RegularExpressions;

namespace LanguageProcessing {
    public static class StringExtensions {
        public static string RemoveAllNonLetters(this string str) {
            return Regex.Replace(str, "[^a-zA-Z ]+", "", RegexOptions.Compiled);
        }
    }
}