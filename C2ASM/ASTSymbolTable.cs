using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2ASM;
using C2ASM.Scopes;
using C2ASM.Helpers;

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
                return sym.GetElementScope().scope;
            }
            return "Symbol not found.";
        }

        public IScope getEnclosingScope()
        {
            return null;
        }

        public void define(ASTElement sym) // add
        {
            if (!symbols.ContainsKey(sym.MNodeName))
                symbols.Add(sym.MNodeName.Trim(new Char[] { '"' }), sym);
        }


        // ** I also have to check if there are in the same scope with the newnode element ** 
        public bool IsDefined(ASTElement newnode, string elementName, Scope scope) // Needs to be fixed(i'll use a LINQ query for this one)
        {
            // Retrieve dictionary values where the keys contain the substring "name"
            // name.Length-2 (e.g.: NT_FUNPREFIX_19) cause we want the last part (serial number) cutted out
            var elements = symbols.Where(kvp => kvp.Key.Contains(TextPreprocessor.RemoveSerialNumb(newnode.MNodeName.Trim(new Char[] { '"' }), '_')))
                                  .Select(kvp => kvp.Value);

            // If elements with the corresponding node-name exist then there is a high chance that they'll have there children's instance saved
            foreach (ASTElement element in elements)
            {
                if (element.m_name_text == elementName && element.GetElementScopeName() == scope.scope)
                {
                    return true;
                }
            }

            return false;
        }

        public ASTElement GetElement(ASTElement newnode, string elementName, Scope scope)
        {

            //List<ASTElement> debug = symbols.Where(kvp => kvp.Value.GetElementScope().Equals(scope)).Select(kvp => kvp.Value).ToList();

            ASTElement element = symbols.Where(kvp => kvp.Value.GetElementScope().Equals(scope) && kvp.Value.m_name_text == elementName)
                                .Select(kvp => kvp.Value)
                                .ToList()[0];     // To return only the first ASTElement (There will always be only 1 or none).

            //ASTElement debugDefinition = symbols.Where(kvp => kvp.Value.GetElementScope() == new GlobalScope() && kvp.Value.m_name_text == "h")
            //     .Select(kvp => kvp.Value)
            //     .ToList()[0];

            return element;
        }

        public ASTElement GetElement(ASTElement newnode, Scope scope, String nodeType)
        {
            List<ASTElement> debug = symbols.Where(kvp => kvp.Value.GetElementScope().Equals(new GlobalScope())).Select(kvp => kvp.Value).ToList();

            ASTElement element = symbols.Where(kvp => kvp.Key.Contains(nodeType) && kvp.Value.m_name_text == scope.scope)
                                .Select(kvp => kvp.Value)
                                .ToList()[0];

            return element;
        }

        public ASTElement resolve(String name) // symbol "lookup" by providing symbols name 
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