namespace Tools.Common.Utils
{
    public static class StringUtils
    {
        public static string Copy(this string stringToCopy)
        {
            return new string(stringToCopy.ToCharArray());
        }
    }
}
