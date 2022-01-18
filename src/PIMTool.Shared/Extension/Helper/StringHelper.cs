using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Extension.Helper
{
    public static class StringHelper
    {
        public static string RemoveWhiteSpaces(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        public static string GetString(byte[] bytes)
        {
            string result = Convert.ToBase64String(bytes);
            return result;
        }

        public static byte[] GetBytes(string str)
        {
            byte[] result = Convert.FromBase64String(str);
            return result;
        }
    }
}
