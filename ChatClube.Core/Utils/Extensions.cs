using System;
using System.Collections.Generic;
using System.Text;

namespace com.chatclube
{
    public static class Extensions
    {
        public static string Truncate(this string value, int maxLength, bool colocarPontinhos = true)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + (colocarPontinhos ? "..." : string.Empty);
        }
    }
}
