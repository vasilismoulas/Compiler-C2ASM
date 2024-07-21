using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2ASM.Scopes;

namespace C2ASM
{
    // EDITED FOR SCOPE
    public class CASTIDENTIFIER : ASTTerminal
    {
        public CASTIDENTIFIER(string idText, ASTElement parent, Scope currentscope) : base(idText, nodeType.NT_EXPRESSION_IDENTIFIER,
           parent, currentscope)
        {
            m_nodeName = GenerateNodeName();
        }
        //VParam: A parameter that keeps parents names inside so we can exclude the Stack<parent> method as like in TestVisitor
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitIDENTIFIER(this, param);
        }
        public override string GenerateNodeName()
        {
            return "\"" + MNodeType + "_" + MSerial + "_(" + m_text + ")\"";
        }
    }

    public class CASTNUMBER : ASTTerminal
    {
        private int m_value;
        public int Value => m_value;

        public CASTNUMBER(string numberText, ASTElement parent) : base(numberText, nodeType.NT_EXPRESSION_NUMBER,
            parent)
        {
            m_value = Int32.Parse(numberText);
            m_nodeName = GenerateNodeName();
        }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitNUMBER(this, param);
        }
        public override string GenerateNodeName()
        {
            return "\"" + MNodeType + "_" + MSerial + "_(" + m_value + ")\"";
        }
    }

    public class CASTCHAR : ASTTerminal
    {
        private string m_value;
        public string Value => m_value;

        public CASTCHAR(string numberText, ASTElement parent) : base(numberText, nodeType.NT_EXPRESSION_CHAR,
            parent)
        {
            m_value = numberText.Replace("\"","");
            m_nodeName = GenerateNodeName();
        }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitCHAR(this, param);
        }
        public override string GenerateNodeName()
        {
            return "\"" + MNodeType + "_" + MSerial + "_(" + m_value + ")\"";
        }
    }

    public class CASTCHAR_TYPE : ASTTerminal
    {
        private int m_value;
        public int Value => m_value;

        public CASTCHAR_TYPE(string numberText, ASTElement parent) : base(numberText, nodeType.NT_CHAR_TYPE,
            parent)
        {
            m_nodeName = GenerateNodeName();
        }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitCHAR_TYPE(this, param);
        }
        public override string GenerateNodeName()
        {
            return "\"" + MNodeType + "_" + MSerial + "_(" + m_value + ")\"";
        }
    }

    public class CASTINT_TYPE : ASTTerminal
    {
        private int m_value;
        public int Value => m_value;

        public CASTINT_TYPE(string numberText, ASTElement parent) : base(numberText, nodeType.NT_INT_TYPE,parent)
        {
            m_nodeName = GenerateNodeName();
        }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitINT_TYPE(this, param);
        }
        public override string GenerateNodeName()
        {
            return "\"" + MNodeType + "_" + MSerial + "_(" + m_value + ")\"";
        }
    }

    public class CASTDOUBLE_TYPE : ASTTerminal
    {
        private int m_value;
        public int Value => m_value;

        public CASTDOUBLE_TYPE(string numberText, ASTElement parent) : base(numberText, nodeType.NT_DOUBLE_TYPE,
            parent)
        {
            m_nodeName = GenerateNodeName();
        }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitDOUBLE_TYPE(this, param);
        }
        public override string GenerateNodeName()
        {
            return "\"" + MNodeType + "_" + MSerial + "_(" + m_value + ")\"";
        }
    }

    public class CASTFLOAT_TYPE : ASTTerminal
    {
        private int m_value;
        public int Value => m_value;

        public CASTFLOAT_TYPE(string numberText, ASTElement parent) : base(numberText, nodeType.NT_FLOAT_TYPE,
            parent)
        {
            m_nodeName = GenerateNodeName();
        }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitFLOAT_TYPE(this, param);
        }
        public override string GenerateNodeName()
        {
            return "\"" + MNodeType + "_" + MSerial + "";
        }
    }

    public class CASTVOID_TYPE : ASTTerminal
    {
        private int m_value;
        public int Value => m_value;

        public CASTVOID_TYPE(string numberText, ASTElement parent) : base(numberText, nodeType.NT_VOID_TYPE,
            parent)
        {
            m_nodeName = GenerateNodeName();
        }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitVOID_TYPE(this, param);
        }
        public override string GenerateNodeName()
        {
            return "\"" + MNodeType + "_" + MSerial + "";
        }
    }

    public class CASTFunctionDefinition : ASTComposite
    {
        public CASTFunctionDefinition(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_FUNCTIOΝDEFINITION, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitFunctionDefinition(this, param);
        }

        public string GetFunctionName()
        {
            CASTIDENTIFIER id = GetChild(contextType.CT_FUNCTIONDEFINITION_IDENTIFIER, 0) as CASTIDENTIFIER;
            return id.M_Text;
        }

        public string[] GetFunctionArgs()
        {
            IEnumerable<CASTIDENTIFIER> args = m_children[GetContextIndex(contextType.CT_FUNCTIONDEFINITION_FARGUMENTS)].Select(a => a as CASTIDENTIFIER);

            return args.Select(a => a.M_Text).ToArray();
        }
    }

    public class CASTExpressionFCALL : ASTComposite
    {
        public CASTExpressionFCALL(string text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_FCALL, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitFCALL(this, param);
        }
    }

    public class CASTFunprefix : ASTComposite
    {
        public CASTFunprefix(string text, ASTElement parent, int numContexts) : base(text, nodeType.NT_FUNPREFIX, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitFunprefix(this, param);
        }
    }

    public class CASTExpressionMultiplication : ASTComposite
    {
        public CASTExpressionMultiplication(string text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_MULTIPLICATION, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitMultiplication(this, param);
        }
    }

    public class CASTExpressionDivision : ASTComposite
    {
        public CASTExpressionDivision(string text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_DIVISION, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitDivision(this, param);
        }
    }

    public class CASTExpressionAddition : ASTComposite
    {
        public CASTExpressionAddition(string text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_ADDITION, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitAddition(this, param);
        }
    }

    public class CASTExpressionSubtraction : ASTComposite
    {
        public CASTExpressionSubtraction(string text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_SUBSTRACTION, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitSubtraction(this, param);
        }
    }
    public class CASTExpressionPlus : ASTComposite
    {
        public CASTExpressionPlus(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_PLUS, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitPLUS(this, param);
        }
    }

    public class CASTExpressionMinus : ASTComposite
    {
        public CASTExpressionMinus(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_MINUS, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitMINUS(this, param);
        }
    }

    public class CASTExpressionInParenthesis : ASTComposite
    {
        public CASTExpressionInParenthesis(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_PARENTHESIS, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitParenthesis(this, param);
        }
    }

    public class CASTExpressionAssign : ASTComposite
    {
        public CASTExpressionAssign(string text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_ASSIGN, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitASSIGN(this, param);
        }
    }
    public class CASTExpressionNot : ASTComposite
    {
        public CASTExpressionNot(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_NOT, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitNOT(this, param);
        }
    }
    public class CASTExpressionAnd : ASTComposite
    {
        public CASTExpressionAnd(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_AND, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitAND(this, param);
        }
    }

    public class CASTExpressionOr : ASTComposite
    {
        public CASTExpressionOr(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_OR, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitOR(this, param);
        }
    }
    public class CASTExpressionGt : ASTComposite
    {
        public CASTExpressionGt(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_GT, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitGT(this, param);
        }
    }

    public class CASTExpressionGte : ASTComposite
    {
        public CASTExpressionGte(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_GTE, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitGTE(this, param);
        }
    }

    public class CASTExpressionLt : ASTComposite
    {
        public CASTExpressionLt(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_LT, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitLT(this, param);
        }
    }

    public class CASTExpressionLte : ASTComposite
    {
        public CASTExpressionLte(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_LTE, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitLTE(this, param);
        }
    }

    public class CASTExpressionEqual : ASTComposite
    {
        public CASTExpressionEqual(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_EQUAL, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitEQUAL(this, param);
        }
    }

    public class CASTExpressionNequal : ASTComposite
    {
        public CASTExpressionNequal(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_EXPRESSION_NEQUAL, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitNEQUAL(this, param);
        }
    }

    public class CASTStatementList : ASTComposite
    {
        public CASTStatementList(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_STATEMENTLIST, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitSTATEMENTLIST(this, param);
        }
    }
    public class CASTCompoundStatement : ASTComposite
    {
        public CASTCompoundStatement(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_COMPOUNDSTATEMENT, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitCOMPOUNDSTATEMENT(this, param);
        }
    }

    public class CASTEpxressionStatement : ASTComposite
    {
        public CASTEpxressionStatement(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_STATEMENT_EXPRESSION, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitSTATEMENTEXPRESSION(this, param);
        }
    }

    public class CASTReturnStatement : ASTComposite
    {
        public CASTReturnStatement(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_STATEMENT_RETURN, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitSTATEMENTRETURN(this, param);
        }
    }

    public class CASTBreakStatement : ASTComposite
    {
        public CASTBreakStatement(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_STATEMENT_BREAK, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitSTATEMENTBREAK(this, param);
        }
    }

    public class CASTWhileStatement : ASTComposite
    {
        public CASTWhileStatement(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_WHILESTATEMENT, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitWHILESTATEMENT(this, param);
        }
    }

    public class CASTIfStatement : ASTComposite
    {
        public CASTIfStatement(String text, ASTElement parent, int numContexts) : base(text, nodeType.NT_IFSTATEMENT, parent, numContexts) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitIFSTATEMENT(this, param);
        }
    }

    public class CASTGlobalStatement : ASTComposite
    {
        public CASTGlobalStatement(String text, nodeType type, ASTElement parent, int numContexts) : base(text, type, parent, numContexts) { }

        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitGLOBALSTATEMENT(this, param);
        }
    }

    public class CASTStatement : ASTComposite
    {
        public CASTStatement(String text, nodeType type, ASTElement parent, int numContexts) : base(text, type, parent, numContexts) { }

        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitSTATEMENT(this, param);
        }
    }


    public class CASTCompileUnit : ASTComposite
    {
        public CASTCompileUnit(string text, ASTElement parent) : base(text, nodeType.NT_COMPILEUNIT, parent, 2) { }
        public override Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param)
        {
            return visitor.VisitCOMPILEUNIT(this, param);
        }
    }


    

}
