using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.Helpers
{
    public static class TypeMapper
    {
        private static readonly Dictionary<string, Type> typeMap = new Dictionary<string, Type>
        {
            { "int", typeof(int) },
            { "float", typeof(float) },
            { "double", typeof(double) },
            { "char", typeof(char) },
            { "void", typeof(void) },
            { "bool", typeof(bool) }
            // Add more mappings as needed
        };

        public static Type GetTypeFromString(string typeName)
        {
            if (typeMap.TryGetValue(typeName, out var type))
            {
                return type;
            }
            throw new ArgumentException($"Type '{typeName}' is not recognized.");
        }
    }
}