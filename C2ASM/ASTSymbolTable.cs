using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM
{

    // Mastermind
	public class ASTSymbolTable : IScope
	{
        // Creating a HashMap with keys of type string and values of type int
        Dictionary<string, ASTElement> symbols;

        public ASTSymbolTable()
		{
             symbols = new Dictionary<string, ASTElement>();
		}

        public String getScopeName(String name)
        {
            if (symbols.TryGetValue(name, out ASTElement sym))
            {
                return sym.GetElementScope();
            }
            return "Symbol not found.";
        }

        public IScope getEnclosingScope()
        {
            return null;
        }

        public void define(ASTElement sym) // add
        {
            if(!symbols.ContainsKey(sym.MNodeName))
              symbols.Add(sym.MNodeName.Trim(new Char[] { '"' }), sym);
        }

        public bool IsDefined(string name) // Needs to be fixed(i'll use a LINQ query for this one)
        {
            // Retrieve dictionary values where the keys contain the substring "name"
            // name.Length-2 (e.g.: NT_FUNPREFIX_19) cause we want the last part (serial number) cutted out
            var values = symbols.Where(kvp => kvp.Key.Contains(RemoveSerialNumb(name,'_')))
                                .Select(kvp => kvp.Value);

            return symbols.ContainsKey(name);
        }

        public ASTElement resolve(String name) // symbol "lookup" by providing symbosl name 
        {
            if (symbols.ContainsKey(name))
               return symbols["name"];

            return null;
        }

        public void free()
        {
            symbols.Clear();
        }

        public override string ToString() // Override ToString method to print dictionary entries
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ASTSymbolTable contents:");

            foreach (var entry in symbols)
            {
                sb.AppendLine($"{entry.Key}: {entry.Value}");
            }

            return sb.ToString();
        }


    }
}