﻿
namespace Common
{
    public static class LinqExtension
    {
        public static string FirstLetterToUpper(this string source)
        {
            if (source == null)
                return null;
            else if (source.Length > 1)
                return char.ToUpper(source[0]) + source.Substring(1);
            else
                return source.ToUpper();
        }
    }
}
