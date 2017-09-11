namespace UserAppService.Utility.Extensions
{
    public class RegexExtensions
    {
        public static string RemoveDirtySpaces(string dirtyString)
        {
            return System.Text.RegularExpressions.Regex.Replace(dirtyString, @"\s+", " ");
        }
        public static string RemoveAllSpaces(string dirtyString)
        {
            return System.Text.RegularExpressions.Regex.Replace(dirtyString, @"\s+", "");
        }
    }
}
