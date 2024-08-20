using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.Helpers
{
    public static class TextPreprocessor
    {
        public static string RemoveSerialNumb(string str, char c)
        {
            int firstIndex = str.IndexOf(c);
            if (firstIndex == -1)
            {
                // The character does not exist in the string
                return str;
            }

            int secondIndex = str.IndexOf(c, firstIndex + 1);
            if (secondIndex == -1)
            {
                // The character does not occur a second time
                return str;
            }

            // Return the substring up to the second occurrence of the character
            return str.Substring(0, secondIndex);
        }
    }
}
