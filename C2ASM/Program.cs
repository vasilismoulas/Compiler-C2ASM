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

            testparser parser = new testparser(cts);

            ASTSymbolTable symtab = new ASTSymbolTable();

            IParseTree tree = parser.compileUnit(symtab);

            TestVisitor visitor = new TestVisitor();

            visitor.Visit(tree);

            Console.WriteLine(tree.ToStringTree(parser)); //print LISP-style tree

            ASTGenerator astGenerator = new ASTGenerator(parser);
            astGenerator.Visit(tree);

            ASTPrinter astPrinter = new ASTPrinter("ASyntaxTree.dot");
            astPrinter.Visit(astGenerator.M_Root);


            
        }
    }
}