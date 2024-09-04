using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM
{
    public abstract class ASTBaseVisitor<Result, VParam>
    {
        public Result Visit(ASTElement node, VParam param = default(VParam))
        {
            return node.Accept(this, param);
        }
        public Result VisitChildren(ASTComposite node, VParam param = default(VParam))
        {
            for (int i = 0; i < node.MChildren.Length; i++)
            {
                foreach (ASTElement item in node.MChildren[i])
                {
                    item.Accept(this, param);
                }
            }
            return default(Result);
        }

        public Result VisitContext(ASTComposite node, contextType ct, VParam param = default(VParam))
        {

            foreach (ASTElement item in node.MChildren[node.GetContextIndex(ct)])
            {
                
                
                
                item.Accept(this, param);
            }
            return default(Result);
        }


        public virtual Result VisitIDENTIFIER(CASTIDENTIFIER node, VParam param = default(VParam))
        {

            return default(Result);
        }
        public virtual Result VisitNUMBER(CASTNUMBER node, VParam param = default(VParam))
        {

            return default(Result);
        }
        public virtual Result VisitCHAR(CASTCHAR node, VParam param = default(VParam))
        {

            return default(Result);
        }

        public virtual Result VisitINT_TYPE(CASTINT_TYPE node, VParam param = default(VParam))
        {

            return default(Result);
        }

        public virtual Result VisitCHAR_TYPE(CASTCHAR_TYPE node, VParam param = default(VParam))
        {

            return default(Result);
        }

        public virtual Result VisitFLOAT_TYPE(CASTFLOAT_TYPE node, VParam param = default(VParam))
        {

            return default(Result);
        }

        public virtual Result VisitDOUBLE_TYPE(CASTDOUBLE_TYPE node, VParam param = default(VParam))
        {

            return default(Result);
        }

        public virtual Result VisitVOID_TYPE(CASTVOID_TYPE node, VParam param = default(VParam))
        {

            return default(Result);
        }


        public virtual Result VisitFunctionDefinition(CASTFunctionDefinition node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitFunctionDeclaration(CASTFunctionDeclaration node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitFUNCTIONBODY(CASTFunctionBody node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitTYPESPECIFIER(CASTTypespecifier node, VParam param = default(VParam))
        {

            return default(Result);
        }

        public virtual Result VisitTYPESPECIFIERINT(CASTTypespecifierInt node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitTYPESPECIFIERDOUBLE(CASTTypespecifierDouble node, VParam param = default(VParam))
        {
            VisitChildren(node, param);

            return default(Result);
        }

        public virtual Result VisitTYPESPECIFIERFLOAT(CASTTypespecifierFloat node, VParam param = default(VParam))
        {
            VisitChildren(node, param);

            return default(Result);
        }

        public virtual Result VisitTYPESPECIFIERCHAR(CASTTypespecifierChar node, VParam param = default(VParam))
        {
            VisitChildren(node, param);

            return default(Result);
        }

        public virtual Result VisitTYPESPECIFIERVOID(CASTTypespecifierVoid node, VParam param = default(VParam))
        {
            VisitChildren(node, param);

            return default(Result);
        }



        //public virtual Result VisitTYPESPECIFIER_INT(CASTTypespecifier_INT node, VParam param = default(VParam))
        //{

        //    return default(Result);
        //}



        public virtual Result VisitFormalArgs(CASTFormalArgs node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitDATADECLARATION(CASTDatadeclaration node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitDATAVALUE(CASTDatavalue node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitMultiplication(CASTExpressionMultiplication node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitFunprefix(CASTFunprefix node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitDivision(CASTExpressionDivision node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitParenthesis(CASTExpressionInParenthesis node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitAddition(CASTExpressionAddition node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitSubtraction(CASTExpressionSubtraction node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitPLUS(CASTExpressionPlus node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitMINUS(CASTExpressionMinus node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitASSIGN(CASTExpressionAssign node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitNOT(CASTExpressionNot node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitAND(CASTExpressionAnd node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitOR(CASTExpressionOr node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitGT(CASTExpressionGt node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitGTE(CASTExpressionGte node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitLT(CASTExpressionLt node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitLTE(CASTExpressionLte node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitEQUAL(CASTExpressionEqual node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitNEQUAL(CASTExpressionNequal node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitSTATEMENTLIST(CASTStatementList node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitCOMPOUNDSTATEMENT(CASTCompoundStatement node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitIFSTATEMENT(CASTIfStatement node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitWHILESTATEMENT(CASTWhileStatement node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitSTATEMENTEXPRESSION(CASTEpxressionStatement node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitSTATEMENTRETURN(CASTReturnStatement node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }
        public virtual Result VisitSTATEMENTBREAK(CASTBreakStatement node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitSTATEMENT(CASTStatement node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        // **MY BONUS ADDITIONS** (including additional data types such as "char")

        public virtual Result VisitGLOBALSTATEMENT(CASTGlobalStatement node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitCOMPILEUNIT(CASTCompileUnit node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

        public virtual Result VisitFCALL(CASTExpressionFCALL node, VParam param = default(VParam))
        {
            VisitChildren(node, param);
            return default(Result);
        }

    }
}
