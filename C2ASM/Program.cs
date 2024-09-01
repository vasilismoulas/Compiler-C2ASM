using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using C_ASM;

namespace C2ASM
{
    public class Program
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
            TypeChecker typechecker = new TypeChecker(parser);
            typechecker.Visit(astGenerator.M_Root);


            // typecheck global boolean should go here
            // 5return;

            //Code Generation
            C2ASMTranslation tr = new C2ASMTranslation();
            tr.VisitCOMPILEUNIT(astGenerator.M_Root as CASTCompileUnit, new TranslationParameters());
            tr.M_TranslatedFile.EmmitStdout();
            StreamWriter trFile = new StreamWriter(Path.GetFileName(args[0] + ".asm"));
            tr.M_TranslatedFile.EmmitToFile(trFile);
            trFile.Close();
            StreamWriter m_streamWriter = new StreamWriter("CodeStructure.dot");
            tr.M_TranslatedFile.PrintStructure(m_streamWriter);
        }

    }
}