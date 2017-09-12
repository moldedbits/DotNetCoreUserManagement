namespace UserAppService.Utility.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsNull<T>(this T value) where T : class
        {
            return value == null;
        }

        public static bool IsNotNull<T>(this T value) where T : class
        {
            return value != null;
        }
    }
}
