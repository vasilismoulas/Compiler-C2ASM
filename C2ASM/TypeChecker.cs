using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM
{
    public class TypeChecker : ASTBaseVisitor<int, object>
    {

        ASTSymbolTable symtab; // "lookups" will be needed (e.g.: functioncalls)

        public TypeChecker(testparser parser)
        {
            symtab = parser.symtab;
        }

        public bool canAssignTo(Type valueType, Type destType, Type promotion)
        {
            // either types are same or value was successfully promoted
            return valueType == destType || promotion == destType;
        }


        public override int VisitCOMPILEUNIT(CASTCompileUnit node, object param)
        {
            

            return 0;
        }

        public override int VisitIDENTIFIER(CASTIDENTIFIER node, object param)
        {
         
            return base.VisitIDENTIFIER(node);
        }

        public override int VisitFCALL(CASTExpressionFCALL node, object param = default(object))
        {

            base.VisitFCALL(node);

            return 0;
        }

        public override int VisitNUMBER(CASTNUMBER node, object param)
        {
          
            return base.VisitNUMBER(node);
        }

        public override int VisitAddition(CASTExpressionAddition node, object param)
        {


            base.VisitAddition(node);

           

            return 0;
        }

        public override int VisitSubtraction(CASTExpressionSubtraction node, object param)
        {
          

            base.VisitSubtraction(node);

         

            return 0;
        }

        public override int VisitMultiplication(CASTExpressionMultiplication node, object param)
        {
         

            base.VisitMultiplication(node);

           

            return 0;
        }

        public override int VisitDivision(CASTExpressionDivision node, object param)
        {
        
            base.VisitDivision(node);

           

            return 0;
        }

        public override int VisitASSIGN(CASTExpressionAssign node, object param)
        {
            //We will use ast symbol table and iterate through current elements children to find the type and bring it up
            // recursively
            Type // we will pass type value in AST generator process

            if (!canAssignTo(rhs.evalType, lhs.evalType, rhs.promoteToType))
            {
                listener.error(text(lhs) + ", " +
                text(rhs) + " have incompatible types in " +
                text((CymbolAST)lhs.getParent()));
            }

            base.VisitASSIGN(node);


            return 0;
        }
        public override int VisitSTATEMENT(CASTStatement node, object param)
        {

          

            base.VisitSTATEMENT(node);
           
            return 0;
        }

        public override int VisitFunctionDefinition(CASTFunctionDefinition node, object param)
        {
          
            base.VisitFunctionDefinition(node);

       
            return 0;
        }

        public override int VisitFormalArgs(CASTFormalArgs node, object param)
        {
           
            base.VisitFormalArgs(node);

           

            return 0;
        }

        public override int VisitDATADECLARATION(CASTDatadeclaration node, object param)
        {
         

            base.VisitDATADECLARATION(node);

            

            return 0;
        }

        public override int VisitFunctionDeclaration(CASTFunctionDeclaration node, object param)
        {
          
            base.VisitFunctionDeclaration(node);

         

            return 0;
        }

        public override int VisitFUNCTIONBODY(CASTFunctionBody node, object param)
        {
         
            base.VisitFUNCTIONBODY(node);

        

            return 0;
        }

        public override int VisitFunprefix(CASTFunprefix node, object param)
        {
           
            base.VisitFunprefix(node);

           

            return 0;
        }

        public override int VisitTYPESPECIFIERINT(CASTTypespecifierInt node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_TYPESPECIFIER_INT_TYPE);

            base.VisitTYPESPECIFIERINT(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitTYPESPECIFIERDOUBLE(CASTTypespecifierDouble node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_TYPESPECIFIER_DOUBLE_TYPE);

            base.VisitTYPESPECIFIERDOUBLE(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitTYPESPECIFIERFLOAT(CASTTypespecifierFloat node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_TYPESPECIFIER_FLOAT_TYPE);

            base.VisitTYPESPECIFIERFLOAT(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitTYPESPECIFIERCHAR(CASTTypespecifierChar node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_TYPESPECIFIER_CHAR_TYPE);


            base.VisitTYPESPECIFIERCHAR(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitTYPESPECIFIERVOID(CASTTypespecifierVoid node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_TYPESPECIFIER_VOID_TYPE);


            base.VisitTYPESPECIFIERVOID(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitINT_TYPE(CASTINT_TYPE node, object param)
        {
            base.VisitINT_TYPE(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitDOUBLE_TYPE(CASTDOUBLE_TYPE node, object param)
        {
            base.VisitDOUBLE_TYPE(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitFLOAT_TYPE(CASTFLOAT_TYPE node, object param)
        {
            base.VisitFLOAT_TYPE(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitCHAR_TYPE(CASTCHAR_TYPE node, object param)
        {
            base.VisitCHAR_TYPE(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitVOID_TYPE(CASTVOID_TYPE node, object param)
        {
            base.VisitVOID_TYPE(node);

            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);

            return 0;
        }

        public override int VisitDATAVALUE(CASTDatavalue node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_DATAVALUE_NUMBER);
            ExtractSubgraphs(node, contextType.CT_DATAVALUE_CHAR);

            base.VisitDATAVALUE(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }


        public override int VisitSTATEMENTLIST(CASTStatementList node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_STATEMENTLIST_STATEMENT);
            base.VisitSTATEMENTLIST(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitSTATEMENTEXPRESSION(CASTEpxressionStatement node, object param = default(object))
        {
            ExtractSubgraphs(node, contextType.CT_STATEMENT_EXPRESSION);
            base.VisitSTATEMENTEXPRESSION(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitSTATEMENTRETURN(CASTReturnStatement node, object param = default(object))
        {
            ExtractSubgraphs(node, contextType.CT_STATEMENT_RETURN);
            base.VisitSTATEMENTRETURN(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitSTATEMENTBREAK(CASTBreakStatement node, object param = default(object))
        {
            base.VisitSTATEMENTBREAK(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitWHILESTATEMENT(CASTWhileStatement node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_WHILESTATEMENT_CONDITION);
            ExtractSubgraphs(node, contextType.CT_WHILESTATEMENT_BODY);
            base.VisitWHILESTATEMENT(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitPLUS(CASTExpressionPlus node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_PLUS);
            base.VisitPLUS(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitMINUS(CASTExpressionMinus node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_MINUS);
            base.VisitMINUS(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitParenthesis(CASTExpressionInParenthesis node, object param = default(object))
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_PARENTHESIS);
            base.VisitParenthesis(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }


        public override int VisitNOT(CASTExpressionNot node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_NOT);
            base.VisitNOT(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitAND(CASTExpressionAnd node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_AND_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_AND_RIGHT);
            base.VisitAND(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitOR(CASTExpressionOr node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_OR_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_OR_RIGHT);
            base.VisitOR(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitGT(CASTExpressionGt node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_GT_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_GT_RIGHT);
            base.VisitGT(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitGTE(CASTExpressionGte node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_GTE_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_GTE_RIGHT);
            base.VisitGTE(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitLT(CASTExpressionLt node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_LT_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_LT_RIGHT);
            base.VisitLT(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitLTE(CASTExpressionLte node, object param)
        {

            ExtractSubgraphs(node, contextType.CT_EXPRESSION_LTE_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_LTE_RIGHT);
            base.VisitLTE(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitEQUAL(CASTExpressionEqual node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_EQUAL_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_EQUAL_RIGHT);
            base.VisitEQUAL(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitNEQUAL(CASTExpressionNequal node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_NEQUAL_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_NEQUAL_RIGHT);
            base.VisitNEQUAL(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }
        public override int VisitCOMPOUNDSTATEMENT(CASTCompoundStatement node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_COMPOUNDSTATEMENT_STATEMENTLIST);
            base.VisitCOMPOUNDSTATEMENT(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitIFSTATEMENT(CASTIfStatement node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_IFSTATEMENT_CONDITION);
            ExtractSubgraphs(node, contextType.CT_IFSTATEMENT_IFCLAUSE);
            ExtractSubgraphs(node, contextType.CT_IFSTATEMENT_ELSECLAUSE);
            base.VisitIFSTATEMENT(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }
    }
}
