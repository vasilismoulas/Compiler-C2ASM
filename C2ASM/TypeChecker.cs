using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM
{
    internal class TypeChecker : testparserBaseVisitor<int>
    {

        ASTSymbolTable symtab; // "loolups" will be needed (e.g.: functioncalls)

        public TypeChecker(testparser parser)
        {
            symtab = parser.symtab;
        }

        public override int VisitExpr_MULDIV(testparser.Expr_MULDIVContext context)
        {

            return 0;
        }

        public override int VisitExpr_PLUSMINUS(testparser.Expr_PLUSMINUSContext context)
        {

            return 0;
        }

        public override int VisitExpr_ASSIGN(testparser.Expr_ASSIGNContext context)
        {

            return 0;
        }

        public override int VisitStatement_DataDeclarationStatement(testparser.Statement_DataDeclarationStatementContext context)
        {
            return 0;
        }


        public override int VisitCustom_FunctionDefinition(testparser.Custom_FunctionDefinitionContext context)
        {
            return 0;
        }


        public override int VisitFunprefix(testparser.FunprefixContext context)
        {
            return 0;
        }

        //public override int VisitTypespecifier(testparser.TypespecifierContext context)
        //{
        //    //Preorder
        //    string nodename = "\"DATATYPE" + m_nodeCounter++ + "\"";
        //    wstream.WriteLine($"{m_parentsName.Peek()}->{nodename};");
        //    m_parentsName.Push(nodename);
        //    base.VisitTypespecifier(context);
        //    //Postorder
        //    m_parentsName.Pop();

        //    return 0;
        //}

        public override int VisitFunctionbody(testparser.FunctionbodyContext context)
        {
            return 0;
        }

        public override int VisitFormalargs(testparser.FormalargsContext context)
        {
            return 0;
        }

        //public override int VisitStatement_ExpressionStatement(testparser.Statement_ExpressionStatementContext context)
        //{
  
        //}
    }
}
