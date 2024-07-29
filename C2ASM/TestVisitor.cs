using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace C2ASM
{
    internal class TestVisitor : testparserBaseVisitor<int>
    {
        private StreamWriter wstream = new StreamWriter("SyntaxTree.dot");
        private Stack<string> m_parentsName = new Stack<string>();
        private int m_nodeCounter = 0;

        public override int VisitCompileUnit(testparser.CompileUnitContext context)
        {
            //Console.WriteLine("hey you are inside CompileUnits function");
            string nodename = "\"CompileUnit_" + m_nodeCounter++ + "\"";
            m_parentsName.Push(nodename);
            wstream.WriteLine("digraph G{");
            base.VisitCompileUnit(context);
            wstream.WriteLine("}");
            m_parentsName.Pop(); //When we remove any of the other grammar rules from the stack, the only grammar rule that will be left is the compileUnit
            wstream.Close();

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif SyntaxTree.dot  -o SyntaxTree.gif";
            // Enter the executable to run, including the complete path
            start.FileName = "dot.exe";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }


            return 0;
        }

        public override int VisitTerminal(ITerminalNode node)
        {
            string nodename;
            switch (node.Symbol.Type)
            {
                case testlexer.IDENTIFIER:
                    nodename = "\"IDENTIFIER_" + m_nodeCounter++ + $"_{node.Symbol.Text}" + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.NUMBER:
                    nodename = "\"NUMBER_" + m_nodeCounter++ + $"_{node.Symbol.Text}" + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.CHAR:
                    nodename = "\"CHAR_" + m_nodeCounter++ + $"_{node.Symbol.Text}" + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.INT_TYPE:
                    nodename = "\"INT_TYPE_" + m_nodeCounter++ + $"_{node.Symbol.Text}" + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.VOID_TYPE:
                    nodename = "\"VOID_TYPE_" + m_nodeCounter++ + $"_{node.Symbol.Text}" + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.CHAR_TYPE:
                    nodename = "\"CHAR_TYPE_" + m_nodeCounter++ + $"_{node.Symbol.Text}" + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.DOUBLE_TYPE:
                    nodename = "\"DOUBLE_TYPE_" + m_nodeCounter++ + $"_{node.Symbol.Text}" + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.FLOAT_TYPE:
                    nodename = "\"FLOAT_TYPE_" + m_nodeCounter++ + $"_{node.Symbol.Text}" + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                default:
                    break;

            }
            return base.VisitTerminal(node);
        }


        public override int VisitExpr_MULDIV(testparser.Expr_MULDIVContext context)
        {
            //Preorder
            string nodename = "";
            switch (context.op.Type)
            {
                case testlexer.MULT:
                    nodename = "\"Multiplication_" + m_nodeCounter++ + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.DIV:
                    nodename = "\"Diviation_" + m_nodeCounter++ + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                default:
                    break;
            }

            //wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);

            base.VisitExpr_MULDIV(context);

            //Postorder
            m_parentsName.Pop()
; return 0;
        }

        public override int VisitExpr_PLUSMINUS(testparser.Expr_PLUSMINUSContext context)
        {

            //Preorder
            string nodename = "";
            switch (context.op.Type)
            {
                case testlexer.MINUS:
                    nodename = "\"Substraction_" + m_nodeCounter++ + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                case testlexer.PLUS:
                    nodename = "\"Addition_" + m_nodeCounter++ + "\"";
                    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
                    break;

                default:
                    break;
            }

            //wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);

            base.VisitExpr_PLUSMINUS(context);

            //Postorder
            m_parentsName.Pop();

            return 0;

        }

        public override int VisitExpr_ASSIGN(testparser.Expr_ASSIGNContext context)
        {
            //Preorder
            string nodename = "\"Assign_" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);

            base.VisitExpr_ASSIGN(context);
            //Postorder
            m_parentsName.Pop();
            return 0;


        }

        public override int VisitExpr_PAREN(testparser.Expr_PARENContext context)
        {
            //Preorder
            string nodename = "\"PAREN_" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);

            base.VisitExpr_PAREN(context);
            //Postorder
            m_parentsName.Pop();
            return 0;
        }

        public override int VisitStatement_DataDeclarationStatement(testparser.Statement_DataDeclarationStatementContext context)
        {
            //Preorder
            string nodename = "\"DATADECLARE_" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);

            base.VisitStatement_DataDeclarationStatement(context);
            //Postorder
            m_parentsName.Pop();
            return 0;
        }


        public override int VisitCustom_FunctionDefinition(testparser.Custom_FunctionDefinitionContext context)
        {
            //Preorder
            string nodename = "\"FUNCTIONDEFINITION" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitCustom_FunctionDefinition(context);
            //Postorder
            m_parentsName.Pop();

            return 0;
        }


        public override int VisitFunprefix(testparser.FunprefixContext context)
        {
            //Preorder
            string nodename = "\"FUNCTIONPREFIX" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitFunprefix(context);
            //Postorder
            m_parentsName.Pop();

            return 0;
        }

        public override int VisitTypespecifier_IntType(testparser.Typespecifier_IntTypeContext context)
        {
            //Preorder
            string nodename = "\"TYPESPECIFIER_INT_TYPE" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitTypespecifier_IntType(context);
            //Postorder
            m_parentsName.Pop();
            return 0;
        }

        public override int VisitTypespecifier_DoubleType(testparser.Typespecifier_DoubleTypeContext context)
        {
            //Preorder
            string nodename = "\"TYPESPECIFIER_DOUBLE_TYPE" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitTypespecifier_DoubleType(context);
            //Postorder
            m_parentsName.Pop();
            return 0;
        }

        public override int VisitTypespecifier_FloatType(testparser.Typespecifier_FloatTypeContext context)
        {
            //Preorder
            string nodename = "\"TYPESPECIFIER_FLOAT_TYPE" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitTypespecifier_FloatType(context);
            //Postorder
            m_parentsName.Pop();
            return 0;
        }

        public override int VisitTypespecifier_CharType(testparser.Typespecifier_CharTypeContext context)
        { 
            //Preorder
            string nodename = "\"TYPESPECIFIER_CHAR_TYPE" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitTypespecifier_CharType(context);
            //Postorder
            m_parentsName.Pop();
            return 0;
        }

        public override int VisitTypespecifier_VoidType(testparser.Typespecifier_VoidTypeContext context)
        {
            //Preorder
            string nodename = "\"TYPESPECIFIER_VOID_TYPE" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitTypespecifier_VoidType(context);
            //Postorder
            m_parentsName.Pop();
            return 0;
        }

        public override int VisitFunctionbody(testparser.FunctionbodyContext context)
        {
            //Preorder
            string nodename = "\"FUNCTIONBODY_" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitFunctionbody(context);
            //Postorder
            m_parentsName.Pop();

            return 0;
        }

        public override int VisitFormalargs(testparser.FormalargsContext context)
        {
            //Preorder
            string nodename = "\"FUNCTIONPARAMETERS_" + m_nodeCounter++ + "\"";
            wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
            m_parentsName.Push(nodename);
            base.VisitFormalargs(context);
            //Postorder
            m_parentsName.Pop();

            return 0;
        }

        //public override int VisitStatement_ExpressionStatement(testparser.Statement_ExpressionStatementContext context)
        //{
        //Preorder
        //    string nodename = "\"STATEMENT_" + m_nodeCounter++ + "\"";
        //    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
        //    m_parentsName.Push(nodename);
        //    base.VisitStatement_ExpressionStatement(context);
        //Postorder
        //    m_parentsName.Pop();

        //    return 0;
        //}



    }
}