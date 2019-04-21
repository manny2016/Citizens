using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core
{
    public static class StringExtension
    {
        public static string TrySubstring(this string text, int start, int length)
        {
            if (text.Length < length)
                return text;
            return text.Substring(start, length);
        }
    }
}
