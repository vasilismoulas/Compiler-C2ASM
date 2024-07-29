using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM
{
	public class ASTSymbolTable : IScope
	{
        // Creating a HashMap with keys of type string and values of type int
        Dictionary<string, ASTElement> symbols;


        public ASTSymbolTable()
		{
             symbols = new Dictionary<string, ASTElement>();
		}

        public String getScopeName()
        {
            return "global";
        }

        public IScope getEnclosingScope()
        {
            return null;
        }

        public void define(ASTElement sym) // adding 
        {
            if(!symbols.ContainsKey(sym.GenerateNodeName()))
              symbols.Add(sym.MNodeName, sym);
        }

        public ASTElement resolve(String name) // symbol "lookup" by providing symbosl name 
        {
            if (symbols.ContainsKey(name))
               return symbols["name"];

            return null;
        }


    }
}