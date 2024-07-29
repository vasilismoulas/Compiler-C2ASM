using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;


namespace C2ASM
{
    class Program
    {
        static void Main(string[] args)
        {

            StreamReader aStreamReader = new StreamReader(args[0]);

            AntlrInputStream antlrInputStream = new AntlrInputStream(aStreamReader);

            testlexer lexer = new testlexer(antlrInputStream);

            CommonTokenStream cts = new CommonTokenStream(lexer);

            ASTSymbolTable symtab = new ASTSymbolTable();

            testparser parser = new testparser(cts, symtab);

            IParseTree tree = parser.compileUnit();

            TestVisitor visitor = new TestVisitor();

            visitor.Visit(tree);

            Console.WriteLine("Syntax Tree: " + tree.ToStringTree(parser)); //print LISP-style tree

            ASTGenerator astGenerator = new ASTGenerator(parser);
            astGenerator.Visit(tree);

            ASTPrinter astPrinter = new ASTPrinter("ASyntaxTree.dot");
            astPrinter.Visit(astGenerator.M_Root);

            Console.WriteLine("Symbol Table entries: " + symtab);

            Console.WriteLine("end ------- ");

            Console.WriteLine("Symbol Table entries Scope information: " + symtab.getScopeName("NT_FUNPREFIX_2"));

            Console.WriteLine("end ------- ");

            //TypeChecking


            //Code Generation

        }

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