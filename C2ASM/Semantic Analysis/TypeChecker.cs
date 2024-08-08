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
            //Type // we will pass type value in AST generator process

            //if (!canAssignTo(rhs.evalType, lhs.evalType, rhs.promoteToType))
            //{
            //    listener.error(text(lhs) + ", " +
            //    text(rhs) + " have incompatible types in " +
            //    text((CymbolAST)lhs.getParent()));
            //}

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


            base.VisitTYPESPECIFIERINT(node);



            return 0;
        }

        public override int VisitTYPESPECIFIERDOUBLE(CASTTypespecifierDouble node, object param)
        {


            base.VisitTYPESPECIFIERDOUBLE(node);



            return 0;
        }

        public override int VisitTYPESPECIFIERFLOAT(CASTTypespecifierFloat node, object param)
        {


            base.VisitTYPESPECIFIERFLOAT(node);



            return 0;
        }

        public override int VisitTYPESPECIFIERCHAR(CASTTypespecifierChar node, object param)
        {



            base.VisitTYPESPECIFIERCHAR(node);



            return 0;
        }

        public override int VisitTYPESPECIFIERVOID(CASTTypespecifierVoid node, object param)
        {



            base.VisitTYPESPECIFIERVOID(node);



            return 0;
        }

        public override int VisitINT_TYPE(CASTINT_TYPE node, object param)
        {
            base.VisitINT_TYPE(node);


            return 0;
        }

        public override int VisitDOUBLE_TYPE(CASTDOUBLE_TYPE node, object param)
        {
            base.VisitDOUBLE_TYPE(node);



            return 0;
        }

        public override int VisitFLOAT_TYPE(CASTFLOAT_TYPE node, object param)
        {
            base.VisitFLOAT_TYPE(node);



            return 0;
        }

        public override int VisitCHAR_TYPE(CASTCHAR_TYPE node, object param)
        {
            base.VisitCHAR_TYPE(node);



            return 0;
        }

        public override int VisitVOID_TYPE(CASTVOID_TYPE node, object param)
        {
            base.VisitVOID_TYPE(node);



            return 0;
        }

        public override int VisitDATAVALUE(CASTDatavalue node, object param)
        {

            base.VisitDATAVALUE(node);

            return 0;
        }


        public override int VisitSTATEMENTLIST(CASTStatementList node, object param)
        {
            base.VisitSTATEMENTLIST(node);

            return 0;
        }

        public override int VisitSTATEMENTEXPRESSION(CASTEpxressionStatement node, object param = default(object))
        {

            base.VisitSTATEMENTEXPRESSION(node);

            return 0;
        }

        public override int VisitSTATEMENTRETURN(CASTReturnStatement node, object param = default(object))
        {

            base.VisitSTATEMENTRETURN(node);

            return 0;
        }

        public override int VisitSTATEMENTBREAK(CASTBreakStatement node, object param = default(object))
        {
            base.VisitSTATEMENTBREAK(node);

            return 0;
        }

        public override int VisitWHILESTATEMENT(CASTWhileStatement node, object param)
        {

            base.VisitWHILESTATEMENT(node);

            return 0;
        }

        public override int VisitPLUS(CASTExpressionPlus node, object param)
        {

            base.VisitPLUS(node);

            return 0;
        }

        public override int VisitMINUS(CASTExpressionMinus node, object param)
        {

            base.VisitMINUS(node);

            return 0;
        }

        public override int VisitParenthesis(CASTExpressionInParenthesis node, object param = default(object))
        {

            base.VisitParenthesis(node);

            return 0;
        }


        public override int VisitNOT(CASTExpressionNot node, object param)
        {

            base.VisitNOT(node);

            return 0;
        }

        public override int VisitAND(CASTExpressionAnd node, object param)
        {

            base.VisitAND(node);

            return 0;
        }

        public override int VisitOR(CASTExpressionOr node, object param)
        {

            base.VisitOR(node);

            return 0;
        }

        public override int VisitGT(CASTExpressionGt node, object param)
        {

            base.VisitGT(node);

            return 0;
        }

        public override int VisitGTE(CASTExpressionGte node, object param)
        {

            base.VisitGTE(node);

            return 0;
        }

        public override int VisitLT(CASTExpressionLt node, object param)
        {

            base.VisitLT(node);

            return 0;
        }

        public override int VisitLTE(CASTExpressionLte node, object param)
        {


            base.VisitLTE(node);

            return 0;
        }

        public override int VisitEQUAL(CASTExpressionEqual node, object param)
        {

            base.VisitEQUAL(node);

            return 0;
        }

        public override int VisitNEQUAL(CASTExpressionNequal node, object param)
        {
            base.VisitNEQUAL(node);

            return 0;
        }
        public override int VisitCOMPOUNDSTATEMENT(CASTCompoundStatement node, object param)
        {

            base.VisitCOMPOUNDSTATEMENT(node);

            return 0;
        }

        public override int VisitIFSTATEMENT(CASTIfStatement node, object param)
        {

            base.VisitIFSTATEMENT(node);

            return 0;
        }
    }
}
