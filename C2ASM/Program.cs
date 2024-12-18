﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;


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

            //Code Generation

        }

    }
}