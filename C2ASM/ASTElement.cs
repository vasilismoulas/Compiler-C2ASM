using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2ASM.Scopes;
using Antlr4.Runtime;


namespace C2ASM
{
    //I have to sort the nodeTypes and contextTypes entries in the same order so we don't face any problem in ASTElements Addchild() function 
    public enum nodeType   //This enumeration refers to the terminal nodes or the ones that are being structured by contexts 
    {
        NA = -1,
        NT_COMPILEUNIT = 0,
        NT_GLOBALSTATEMENT = 2,

        NT_FUNCTIONDECLARATION = 3,
        NT_FUNCTIOΝDEFINITION = 5,  //5
        NT_FUNPREFIX = 9,
        NT_FUNCTIONBODY = 11,
        NT_STATEMENT = 12,
        NT_STATEMENT_RETURN = 13,
        NT_STATEMENT_BREAK = 14,
        NT_IFSTATEMENT = 15,
        NT_WHILESTATEMENT = 18,
        NT_COMPOUNDSTATEMENT = 20,
        NT_STATEMENTLIST = 22,
        NT_DATADECLARATION = 23,    //23

        NT_DATAVALUE = 26,

        NT_TYPESPECIFIER = 28,
        NT_INT_TYPE = 34,
        NT_DOUBLE_TYPE = 35,
        NT_FLOAT_TYPE = 36,
        NT_CHAR_TYPE = 37,
        NT_VOID_TYPE = 38,

        NT_STATEMENT_EXPRESSION = 33,
        NT_EXPRESSION_NUMBER = 31,
        NT_EXPRESSION_IDENTIFIER = 32,
        NT_EXPRESSION_CHAR = 33,
        NT_EXPRESSION_FCALL = 34,
        NT_EXPRESSION_MULTIPLICATION = 36,
        NT_EXPRESSION_DIVISION = 38,
        NT_EXPRESSION_ADDITION = 40,
        NT_EXPRESSION_SUBSTRACTION = 42,
        NT_EXPRESSION_PLUS = 44,
        NT_EXPRESSION_MINUS = 45,
        NT_EXPRESSION_PARENTHESIS = 46,
        NT_EXPRESSION_ASSIGN = 47,
        NT_EXPRESSION_NOT = 49,
        NT_EXPRESSION_AND = 51,
        NT_EXPRESSION_OR = 53,
        NT_EXPRESSION_GT = 55,
        NT_EXPRESSION_GTE = 57,
        NT_EXPRESSION_LT = 59,
        NT_EXPRESSION_LTE = 61,
        NT_EXPRESSION_EQUAL = 63,
        NT_EXPRESSION_NEQUAL = 65,
        NT_FARGUMENTS = 56

    };

    public enum contextType   //This enumeration refers the ones that structure higher in a hierarchical meaning nodes
    {
        NA = -1,
        CT_COMPILEUNIT_FUNCTIONDEFINITION,
        CT_COMPILEUNIT_GLOBALSTATEMENT,
        CT_GLOBALSTATEMENT_FUNCTIONDECLARATION,
        CT_FUNCTIONDECLARATION_FUNPREFIX,
        CT_FUNCTIONDECLARATION_FARGUMENTS,
        CT_FUNCTIONDEFINITION_FUNPREFIX,
        CT_FUNCTIONDEFINITION_IDENTIFIER,
        CT_FUNCTIONDEFINITION_FARGUMENTS,
        CT_FUNCTIONDEFINITION_BODY,
        CT_FUNPREFIX_TYPESPECIFIER,
        CT_FUNPREFIX_IDENTIFIER,
        CT_FUNCTIONBODY_STATEMENT,

        CT_STATEMENT,

        CT_STATEMENT_RETURN,
        CT_STATEMENT_BREAK,

        CT_IFSTATEMENT_CONDITION,
        CT_IFSTATEMENT_IFCLAUSE,
        CT_IFSTATEMENT_ELSECLAUSE,

        CT_WHILESTATEMENT_CONDITION,
        CT_WHILESTATEMENT_BODY,

        CT_COMPOUNDSTATEMENT_STATEMENTLIST,
        CT_STATEMENT_COMPOUNDSTATEMENT,
        CT_STATEMENTLIST_STATEMENT,

        CT_DATADECLARATION_TYPESPECIFIER,
        CT_DATADECLARATION_IDENTIFIER,
        CT_DATADECLARATION_DATAVALUE,
        CT_DATAVALUE_NUMBER,
        CT_DATAVALUE_CHAR,


        CT_TYPESPECIFIER_INT_TYPE,
        CT_TYPESPECIFIER_DOUBLE_TYPE,
        CT_TYPESPECIFIER_FLOAT_TYPE,
        CT_TYPESPECIFIER_CHAR_TYPE,
        CT_TYPESPECIFIER_VOID_TYPE,

        CT_STATEMENT_EXPRESSION,

        CT_EXPRESSION_NUMBER,
        CT_EXPRESSION_IDENTIFIER,
        CT_EXPRESSION_MULTIPLICATION_LEFT,
        CT_EXPRESSION_MULTIPLICATION_RIGHT,
        CT_EXPRESSION_DIVISION_LEFT,
        CT_EXPRESSION_DIVISION_RIGHT,
        CT_EXPRESSION_ADDITION_LEFT,
        CT_EXPRESSION_ADDITION_RIGHT,
        CT_EXPRESSION_SUBTRACTION_LEFT,
        CT_EXPRESSION_SUBTRACTION_RIGHT,
        CT_EXPRESSION_PLUS,
        CT_EXPRESSION_MINUS,
        CT_EXPRESSION_PARENTHESIS,
        CT_EXPRESSION_ASSIGN_LVALUE,
        CT_EXPRESSION_ASSIGN_EXPRESSION,
        CT_EXPRESSION_NOT,
        CT_EXPRESSION_AND_LEFT,
        CT_EXPRESSION_AND_RIGHT,
        CT_EXPRESSION_OR_LEFT,
        CT_EXPRESSION_OR_RIGHT,
        CT_EXPRESSION_GT_LEFT,

        CT_EXPRESSION_GT_RIGHT,

        CT_FARGUMENTS_DATADECLARATION,

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
        CT_EXPRESSION_FCALLNAME,
        CT_EXPRESSION_FCALLARGS,
    }

    public abstract class ASTElement
    {
        private int m_serial;
        private static int ms_serialCounter = 0;
        private nodeType m_nodeType;
        private ASTElement m_parent;
        private Scope m_scope;
        //private Type m_type;
        public Type m_type { get; set; }
        //public ParserRuleContext m_context { get; set; }
        protected string m_nodeName;
        protected string m_text;
        List<ASTElement>[] children_list = null;

        public string m_name_text { get; set; }


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

        public List<ASTElement>[] MChildrenList => children_list;

        //public Type M_Type => m_type;

        protected ASTElement(string text, nodeType type, ASTElement parent, Scope currentscope)
        {
            m_nodeType = type;
            m_parent = parent;
            m_serial = ms_serialCounter++;
            m_text = text;
            m_scope = currentscope;
            m_type = null;
            m_name_text = null;
            children_list = null;
            //m_context = null; // i'll have to intergrate it inside the formal arguments of the constructor.
        }

        public List<ASTElement>[] GetChildrenList()
        {
            return children_list;
        }

        protected void SetChildrenList(List<ASTElement>[] children_list)
        {
            this.children_list= children_list;
        }

        public String GetElementScopeName()
        {
            return m_scope.scope;
        }

        public Scope GetElementScope()
        {
            return m_scope;
        }

    }
    public abstract class ASTComposite : ASTElement
    {
        protected List<ASTElement>[] m_children;

        public List<ASTElement>[] MChildren => m_children;

        protected ASTComposite(string text, nodeType type, ASTElement parent, Scope currentscope, int numContexts) : base(text, type, parent, currentscope)
        {
            m_children = new List<ASTElement>[numContexts];
            for (int i = 0; i < numContexts; i++)
            {
                m_children[i] = new List<ASTElement>();
            }
            m_nodeName = GenerateNodeName();
            base.SetChildrenList(m_children);
        }

        internal int GetContextIndex(contextType ct)
        {
            int index;
            index = (int)ct - (int)MNodeType;
            Console.WriteLine(ct + "  " + MNodeType);
            return index;
        }

        internal void AddChild(ASTElement child, contextType ct)
        {
            int index = GetContextIndex(ct);
            m_children[index].Add(child);
            Console.WriteLine(index + "   " + m_children[index] + "    " + child);

        }

        internal ASTElement GetChild(contextType ct, int index)
        {
            int i = GetContextIndex(ct);
            Console.WriteLine(m_children);
            ; return m_children[i][index];
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

        protected ASTTerminal(string text, nodeType type, ASTElement parent, Scope currentscope) : base(text, type, parent, currentscope)
        {
            m_text = text;
        }

    }
}