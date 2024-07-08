using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM
{    
    //I have to sort the nodeTypes and contextTypes entries in the same order so we don't face any problem in ASTElements Addchild() function 
    public enum nodeType   //This enumeration refers to the terminal nodes or the ones that are being structured by contexts 
    {
        NA = -1,
        NT_COMPILEUNIT,
        NT_GLOBALSTATEMENT,
        NT_FUNPREFIX,
        NT_FUNCTIOΝDEFINITION,
        NT_STATEMENT,
        NT_IFSTATEMENT,
        NT_WHILESTATEMENT,
        NT_COMPOUNDSTATEMENT,
        NT_STATEMENTLIST,
        NT_DATADECLARATION,
        NT_DATAVALUE,
        NT_TYPESPECIFIER,
        NT_EXPRESSION_CHAR,
        NT_EXPRESSION_NUMBER,
        NT_EXPRESSION_IDENTIFIER,
        NT_EXPRESSION_DIVISION,
        NT_EXPRESSION_MULTIPLICATION,
        NT_EXPRESSION_ADDITION,
        NT_EXPRESSION_SUBSTRACTION,
        NT_EXPRESSION_PLUS,
        NT_EXPRESSION_MINUS,
        NT_EXPRESSION_ASSIGN,
        NT_EXPRESSION_NOT,
        NT_EXPRESSION_AND,
        NT_EXPRESSION_OR,
        NT_EXPRESSION_GT,
        NT_EXPRESSION_GTE,
        NT_EXPRESSION_LT,
        NT_EXPRESSION_LTE,
        NT_EXPRESSION_EQUAL,
        NT_EXPRESSION_NEQUAL,
        NT_STATEMENT_EXPRESSION,
        NT_STATEMENT_RETURN,
        NT_EXPRESSION_PARENTHESIS,
        NT_STATEMENT_BREAK,
        NT_EXPRESSION_FCALL,
        NT_VOID_TYPE,
        NT_FLOAT_TYPE,
        NT_DOUBLE_TYPE,
        NT_INT_TYPE,
        NT_CHAR_TYPE
        // **MY BONUS ADDITIONS** (including additional data types such as "char")

       
    };

    public enum contextType   //This enumeration refers the ones that structure higher in a hierarchical meaning nodes
    {
        NA = -1,
        CT_COMPILEUNIT_FUNCTIONDEFINITION,
        CT_COMPILEUNIT_GLOBALSTATEMENT,
        //CT_GOBALSTATEMENT_FUNCTIONDECLARATION,
        //CT_FUNPREFIX_TYPESPECIFIER,
        CT_FUNPREFIX_IDENTIFIER,
        CT_FUNCTIONDEFINITION_FUNPREFIX,
        CT_FUNCTIONDEFINITION_IDENTIFIER,
        CT_FUNCTIONDEFINITION_FARGUMENTS,
        CT_FUNCTIONDEFINITION_BODY,
        CT_FUNCTIONDEFINITION_RETURNTYPE,
        CT_STATEMENT,
        CT_IFSTATEMENT_CONDITION,
        CT_IFSTATEMENT_IFCLAUSE,
        CT_IFSTATEMENT_ELSECLAUSE,
        CT_WHILESTATEMENT_CONDITION,
        CT_WHILESTATEMENT_BODY,
        CT_COMPOUNDSTATEMENT,
        CT_STATEMENTLIST,
        CT_DATADECLARATION_DATATYPE,
        CT_DATADECLARATION_LEFT,
        CT_DATADECLARATION_RIGHT,
        CT_EXPRESSION_NUMBER,
        CT_EXPRESSION_IDENTIFIER,
        CT_EXPRESSION_DIVISION_LEFT,
        CT_EXPRESSION_DIVISION_RIGHT,
        CT_EXPRESSION_MULTIPLICATION_LEFT,
        CT_EXPRESSION_MULTIPLICATION_RIGHT,
        CT_EXPRESSION_ADDITION_LEFT,
        CT_EXPRESSION_ADDITION_RIGHT,
        CT_EXPRESSION_SUBTRACTION_LEFT,
        CT_EXPRESSION_SUBTRACTION_RIGHT,
        CT_EXPRESSION_PLUS,
        CT_EXPRESSION_MINUS,
        CT_EXPRESSION_ASSIGN_LVALUE,
        CT_EXPRESSION_ASSIGN_EXPRESSION,
        CT_EXPRESSION_NOT,
        CT_EXPRESSION_AND_LEFT,
        CT_EXPRESSION_AND_RIGHT,
        CT_EXPRESSION_OR_LEFT,
        CT_EXPRESSION_OR_RIGHT,
        CT_EXPRESSION_GT_LEFT,
        CT_EXPRESSION_GT_RIGHT,
        CT_EXPRESSION_GTE_LEFT,
        CT_EXPRESSION_GTE_RIGHT,
        CT_EXPRESSION_LT_LEFT,
        CT_EXPRESSION_LT_RIGHT,
        CT_EXPRESSION_LTE_LEFT,
        CT_EXPRESSION_LTE_RIGHT,
        CT_EXPRESSION_EQUAL_LEFT,
        CT_EXPRESSION_EQUAL_RIGHT,
        CT_EXPRESSION_NEQUAL_LEFT,
        CT_EXPRESSION_NEQUAL_RIGHT,
        CT_STATEMENT_EXPRESSION,
        CT_STATEMENT_RETURN,
        CT_EXPRESSION_PARENTHESIS,
        CT_EXPRESSION_FCALLNAME,
        CT_EXPRESSION_FCALLARGS,
                                        // **MY BONUS ADDITIONS** (including additional data types such as "char") 
       

    }

    public abstract class ASTElement
    {
        private int m_serial;
        private static int ms_serialCounter = 0;
        private nodeType m_nodeType;
        private ASTElement m_parent;
        protected string m_nodeName;
        protected string m_text;

        public nodeType MNodeType => m_nodeType;

        public virtual string GenerateNodeName()
        {
            return "\"" + m_nodeType + "_" + m_serial + "\"";
        }

        public abstract Result Accept<Result, VParam>(ASTBaseVisitor<Result, VParam> visitor, VParam param);

        public ASTElement MParent
        {
            get { return m_parent; }
        }

        public string MNodeName => m_nodeName;
        public int MSerial => m_serial;

        public string M_Text => m_text;

        protected ASTElement(string text, nodeType type, ASTElement parent)
        {
            m_nodeType = type;
            m_parent = parent;
            m_serial = ms_serialCounter++;
            m_text = text;
        }
    }
    public abstract class ASTComposite : ASTElement
    {
        protected List<ASTElement>[] m_children;

        public List<ASTElement>[] MChildren => m_children;

        protected ASTComposite(string text, nodeType type, ASTElement parent, int numContexts) : base(text, type, parent)
        {
            m_children = new List<ASTElement>[numContexts];
            for (int i = 0; i < numContexts; i++)
            {
                m_children[i] = new List<ASTElement>();
            }
            m_nodeName = GenerateNodeName();
        }

        internal int GetContextIndex(contextType ct)
        {
            int index;
            index = (int)ct - (int)MNodeType;
            Console.WriteLine(ct+"  "+ MNodeType);
            return index;
        }

        internal void AddChild(ASTElement child, contextType ct)
        {
            int index = GetContextIndex(ct);
            m_children[index].Add(child);
            Console.WriteLine(index+"   "+m_children[index] +"    "+ child);

        }

        internal ASTElement GetChild(contextType ct, int index)
        {
            int i = GetContextIndex(ct);
            return m_children[i][index];
        }

        internal ASTElement[] GetContextChildren(contextType ct)
        {
            int i = GetContextIndex(ct);
            return m_children[i].ToArray();
        }
    }

    public abstract class ASTTerminal : ASTElement
    {
        private string m_text;
        public string M_Text => base.m_text;

        protected ASTTerminal(string text, nodeType type, ASTElement parent) : base(text, type, parent)
        {
            m_text = text;
        }

    }
}
