using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2ASM.Scopes;
using C2ASM.Helpers;
using Antlr4.Runtime.Tree;


namespace C2ASM
{
    class ASTGenerator : testparserBaseVisitor<int>
    {
        private ASTComposite m_root;

        private ASTSymbolTable m_symbolTable;

        Stack<ASTComposite> m_parents = new Stack<ASTComposite>();

        Stack<contextType> m_parentContext = new Stack<contextType>();

        // **Scope**
        Stack<Scope> m_parents_scope = new Stack<Scope>();

        public ASTComposite M_Root => m_root;

        public ASTGenerator(testparser parser)
        {
            if (parser.symtab == null)
            {
                Console.WriteLine("symbol table doesn't exist");
                Environment.Exit(1);
            }
            m_symbolTable = parser.symtab;
        }

        // In Each Visit function invocation we add the corresponding ast element inside the symbol table
        public override int VisitCompileUnit(testparser.CompileUnitContext context)
        {
            Scope currentscope = new GlobalScope();// push scope
            CASTCompileUnit newnode = new CASTCompileUnit(context.GetText(), null, currentscope);
            m_root = newnode;
            m_parents_scope.Push(currentscope);

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);
            this.VisitElementsInContext(context.functionDefinition(), m_parentContext, contextType.CT_COMPILEUNIT_FUNCTIONDEFINITION);
            this.VisitElementsInContext(context.globalstatement(), m_parentContext, contextType.CT_COMPILEUNIT_GLOBALSTATEMENT);
            m_parents.Pop();
            m_parents_scope.Pop();
            return 0;
        }

        public override int VisitGlobalstatement(testparser.GlobalstatementContext context)
        {
            CASTGlobalStatement newnode = new CASTGlobalStatement(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_root = newnode;

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);
            this.VisitElementInContext(context.functionDeclaration(), m_parentContext, contextType.CT_GLOBALSTATEMENT_FUNCTIONDECLARATION);
            m_parents.Pop();
            return 0;
        }

        public override int VisitFunctionDeclaration(testparser.FunctionDeclarationContext context)
        {
            // Extracting the return type from the context
            Type element_type = TypeMapper.GetTypeFromString(context.funprefix().typespecifier().GetText());

            CASTFunctionDeclaration newnode = new CASTFunctionDeclaration(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_root = newnode;

            // set Type
            newnode.m_type = element_type;

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);
            this.VisitElementInContext(context.funprefix(), m_parentContext, contextType.CT_FUNCTIONDECLARATION_FUNPREFIX);
            if (context.formalargs() != null)
                this.VisitElementInContext(context.formalargs(), m_parentContext, contextType.CT_FUNCTIONDECLARATION_FARGUMENTS);
            m_parents.Pop();
            return 0;
        }

        // ** Function Definitions**
        public override int VisitCustom_FunctionDefinition(testparser.Custom_FunctionDefinitionContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            CASTFunctionDefinition newnode = new CASTFunctionDefinition(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 4);

            m_parent.AddChild(newnode, m_parentContext.Peek());

            // Retrieve the function name from the funprefix context
            var functionName = context.funprefix().IDENTIFIER().GetText();

            // Define the function in the symbol table, checking for duplicates
            if (m_symbolTable.IsDefined(newnode, functionName, newnode.GetElementScope()))
            {
                throw new Exception($"Duplicate function name: {newnode.MNodeName} already defined.");
            }

            m_symbolTable.define(newnode);         // add symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.funprefix(), m_parentContext, contextType.CT_FUNCTIONDEFINITION_FUNPREFIX);

            // Retrieve the function name from the funprefix context
            //var functionName = context.funprefix().IDENTIFIER().GetText();
            Scope currentscope = new LocalScope(functionName);
            //Scope currentscope = new LocalScope(newnode.GetFunctionName());
            m_parents_scope.Push(currentscope);

            if (context.formalargs() != null)
                this.VisitElementInContext(context.formalargs(), m_parentContext, contextType.CT_FUNCTIONDEFINITION_FARGUMENTS);
            if (context.functionbody() != null)
                this.VisitElementInContext(context.functionbody(), m_parentContext, contextType.CT_FUNCTIONDEFINITION_BODY);
            m_parents.Pop();
            m_parents_scope.Pop();
            return 0;
        }

        public override int VisitFunprefix(testparser.FunprefixContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTFunprefix newnode = new CASTFunprefix(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);
            this.VisitElementInContext(context.typespecifier(), m_parentContext, contextType.CT_FUNPREFIX_TYPESPECIFIER);
            this.VisitTerminalInContext(context, context.IDENTIFIER().Symbol, m_parentContext, contextType.CT_FUNPREFIX_IDENTIFIER);
            m_parents.Pop();
            return 0;
        }

        //Scope is not right here.I got to check it again
        public override int VisitTypespecifier_IntType(testparser.Typespecifier_IntTypeContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTTypespecifierInt newnode = new CASTTypespecifierInt(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitTerminalInContext(context, context.INT_TYPE().Symbol, m_parentContext, contextType.CT_TYPESPECIFIER_INT_TYPE);

            m_parents.Pop();
            return 0;
        }

        public override int VisitTypespecifier_DoubleType(testparser.Typespecifier_DoubleTypeContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTTypespecifierDouble newnode = new CASTTypespecifierDouble(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitTerminalInContext(context, context.DOUBLE_TYPE().Symbol, m_parentContext, contextType.CT_TYPESPECIFIER_DOUBLE_TYPE);

            m_parents.Pop();
            return 0;
        }

        public override int VisitTypespecifier_FloatType(testparser.Typespecifier_FloatTypeContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTTypespecifierFloat newnode = new CASTTypespecifierFloat(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 3);
            m_parent.AddChild(newnode, m_parentContext.Peek());
            m_parents.Push(newnode);

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            this.VisitTerminalInContext(context, context.FLOAT_TYPE().Symbol, m_parentContext, contextType.CT_TYPESPECIFIER_FLOAT_TYPE);
            m_parents.Pop();
            return 0;
        }

        public override int VisitTypespecifier_CharType(testparser.Typespecifier_CharTypeContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTTypespecifierChar newnode = new CASTTypespecifierChar(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 4);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitTerminalInContext(context, context.CHAR_TYPE().Symbol, m_parentContext, contextType.CT_TYPESPECIFIER_CHAR_TYPE);

            m_parents.Pop();
            return 0;
        }

        public override int VisitTypespecifier_VoidType(testparser.Typespecifier_VoidTypeContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTTypespecifierVoid newnode = new CASTTypespecifierVoid(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 5);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitTerminalInContext(context, context.VOID_TYPE().Symbol, m_parentContext, contextType.CT_TYPESPECIFIER_VOID_TYPE);

            m_parents.Pop();
            return 0;
        }


        public override int VisitFormalargs(testparser.FormalargsContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTFormalArgs newnode = new CASTFormalArgs(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);
            this.VisitElementsInContext(context.datadeclaration(), m_parentContext, contextType.CT_FARGUMENTS_DATADECLARATION);
            m_parents.Pop();
            return 0;
        }

        public override int VisitDatadeclaration(testparser.DatadeclarationContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTDatadeclaration newnode = new CASTDatadeclaration(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 3);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.typespecifier(), m_parentContext, contextType.CT_DATADECLARATION_TYPESPECIFIER);
            this.VisitTerminalInContext(context, context.IDENTIFIER().Symbol, m_parentContext, contextType.CT_DATADECLARATION_IDENTIFIER);
            if (context.datavalue() != null)
                this.VisitElementInContext(context.datavalue(), m_parentContext, contextType.CT_DATADECLARATION_DATAVALUE);

            m_parents.Pop();
            return 0;
        }

        public override int VisitDatavalue_Number(testparser.Datavalue_NumberContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTDatavalue newnode = new CASTDatavalue(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitTerminalInContext(context, context.NUMBER().Symbol, m_parentContext, contextType.CT_DATAVALUE_NUMBER);

            m_parents.Pop();
            return 0;
        }

        public override int VisitDatavalue_Char(testparser.Datavalue_CharContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTDatavalue newnode = new CASTDatavalue(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitTerminalInContext(context, context.CHAR().Symbol, m_parentContext, contextType.CT_DATAVALUE_CHAR);

            m_parents.Pop();
            return 0;
        }

        public override int VisitFunctionbody(testparser.FunctionbodyContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTFunctionBody newnode = new CASTFunctionBody(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementsInContext(context.statement(), m_parentContext, contextType.CT_FUNCTIONBODY_STATEMENT);
            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_MULDIV(testparser.Expr_MULDIVContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            if (context.op.Type == testparser.MULT)
            {
                CASTExpressionMultiplication newnode = new CASTExpressionMultiplication(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
                m_parent.AddChild(newnode, m_parentContext.Peek());

                m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

                m_parents.Push(newnode);
                this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_MULTIPLICATION_LEFT);
                this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_MULTIPLICATION_RIGHT);
            }
            else if (context.op.Type == testparser.DIV)
            {
                CASTExpressionDivision newnode = new CASTExpressionDivision(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
                m_parent.AddChild(newnode, m_parentContext.Peek());

                m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

                m_parents.Push(newnode);
                this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_DIVISION_LEFT);
                this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_DIVISION_RIGHT);
            }

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_PAREN(testparser.Expr_PARENContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionInParenthesis newnode = new CASTExpressionInParenthesis(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_EXPRESSION_PARENTHESIS);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_PLUSMINUS(testparser.Expr_PLUSMINUSContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            if (context.op.Type == testparser.MINUS)
            {
                //???
                CASTExpressionSubtraction newnode = new CASTExpressionSubtraction(context.GetText(), m_parents.Peek(), 2, m_parents_scope.Peek());
                m_parent.AddChild(newnode, m_parentContext.Peek());

                m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

                m_parents.Push(newnode);

                this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_SUBTRACTION_LEFT);
                this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_SUBTRACTION_RIGHT);
            }
            else if (context.op.Type == testparser.PLUS)
            {
                CASTExpressionAddition newnode = new CASTExpressionAddition(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
                m_parent.AddChild(newnode, m_parentContext.Peek());

                m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

                m_parents.Push(newnode);

                this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_ADDITION_LEFT);
                this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_ADDITION_RIGHT);
            }



            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_PLUS(testparser.Expr_PLUSContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionPlus newnode = new CASTExpressionPlus(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);
            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_EXPRESSION_PLUS);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_MINUS(testparser.Expr_MINUSContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionMinus newnode = new CASTExpressionMinus(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_EXPRESSION_MINUS);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_ASSIGN(testparser.Expr_ASSIGNContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionAssign newnode = new CASTExpressionAssign(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitTerminalInContext(context, context.IDENTIFIER().Symbol, m_parentContext, contextType.CT_EXPRESSION_ASSIGN_LVALUE);
            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_EXPRESSION_ASSIGN_EXPRESSION);
            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_FCALL(testparser.Expr_FCALLContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionFCALL newnode = new CASTExpressionFCALL(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitTerminalInContext(context, context.IDENTIFIER().Symbol, m_parentContext, contextType.CT_EXPRESSION_FCALLNAME);
            this.VisitElementInContext(context.args(), m_parentContext, contextType.CT_EXPRESSION_FCALLARGS);
            m_parents.Pop();
            return 0;
        }

        //public override int VisitArgs(testparser.ArgsContext context) {
        //    ASTComposite m_parent = m_parents.Peek();
        //    CASTArgs newnode = new CASTArgs(context.GetText(), m_parents.Peek(), 2);
        //    m_parent.AddChild(newnode, m_parentContext.Peek());
        //    m_parents.Push(newnode);

        //    this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_EX);

        //    m_parents.Pop();
        //    return 0;
        //}

        public override int VisitExpr_NOT(testparser.Expr_NOTContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionNot newnode = new CASTExpressionNot(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_EXPRESSION_NOT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_AND(testparser.Expr_ANDContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionAnd newnode = new CASTExpressionAnd(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_AND_LEFT);
            this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_AND_RIGHT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_OR(testparser.Expr_ORContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionOr newnode = new CASTExpressionOr(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_OR_LEFT);
            this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_OR_RIGHT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_GT(testparser.Expr_GTContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionGt newnode = new CASTExpressionGt(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_GT_LEFT);
            this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_GT_RIGHT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_GTE(testparser.Expr_GTEContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionGte newnode = new CASTExpressionGte(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_GTE_LEFT);
            this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_GTE_RIGHT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_LT(testparser.Expr_LTContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionLt newnode = new CASTExpressionLt(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_LT_LEFT);
            this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_LT_RIGHT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_LTE(testparser.Expr_LTEContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionLte newnode = new CASTExpressionLte(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_LTE_LEFT);
            this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_LTE_RIGHT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_EQUAL(testparser.Expr_EQUALContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionEqual newnode = new CASTExpressionEqual(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_EQUAL_LEFT);
            this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_EQUAL_RIGHT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitExpr_NEQUAL(testparser.Expr_NEQUALContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTExpressionNequal newnode = new CASTExpressionNequal(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(0), m_parentContext, contextType.CT_EXPRESSION_NEQUAL_LEFT);
            this.VisitElementInContext(context.expr(1), m_parentContext, contextType.CT_EXPRESSION_NEQUAL_RIGHT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitStatementList(testparser.StatementListContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTStatementList newnode = new CASTStatementList(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementsInContext(context.statement(), m_parentContext, contextType.CT_STATEMENTLIST_STATEMENT);

            m_parents.Pop();
            return 0;
        }

        public override int VisitCompoundStatement(testparser.CompoundStatementContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTCompoundStatement newnode = new CASTCompoundStatement(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.statementList(), m_parentContext, contextType.CT_COMPOUNDSTATEMENT_STATEMENTLIST);

            m_parents.Pop();
            return 0;
        }

        public override int VisitStatement_BreakStatement(testparser.Statement_BreakStatementContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTBreakStatement newnode = new CASTBreakStatement(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 0);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            return 0;
        }

        public override int VisitWhilestatement(testparser.WhilestatementContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTWhileStatement newnode = new CASTWhileStatement(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 2);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);
            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_WHILESTATEMENT_CONDITION);
            this.VisitElementInContext(context.statement(), m_parentContext, contextType.CT_WHILESTATEMENT_BODY);

            m_parents.Pop();
            return 0;
        }

        public override int VisitIfstatement(testparser.IfstatementContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTIfStatement newnode = new CASTIfStatement(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 3);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_IFSTATEMENT_CONDITION);
            this.VisitElementInContext(context.statement(0), m_parentContext, contextType.CT_IFSTATEMENT_IFCLAUSE);
            if (context.statement(1) != null)
                this.VisitElementInContext(context.statement(1), m_parentContext, contextType.CT_IFSTATEMENT_ELSECLAUSE);
            m_parents.Pop();
            return 0;
        }



        public override int VisitStatement_ReturnStatement(testparser.Statement_ReturnStatementContext context)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTReturnStatement newnode = new CASTReturnStatement(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_STATEMENT_RETURN);

            m_parents.Pop();
            return 0;
        }


        public override int VisitStatement_ExpressionStatement(testparser.Statement_ExpressionStatementContext context)
        {

            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            CASTEpxressionStatement newnode = new CASTEpxressionStatement(context.GetText(), m_parents.Peek(), m_parents_scope.Peek(), 1);
            m_parent.AddChild(newnode, m_parentContext.Peek());

            m_symbolTable.define(newnode);         // push symbol(inside SymbolTable)

            m_parents.Push(newnode);

            this.VisitElementInContext(context.expr(), m_parentContext, contextType.CT_STATEMENT_EXPRESSION);

            m_parents.Pop();
            //return 0;
            return base.VisitStatement_ExpressionStatement(context);
        }


        public override int VisitTerminal(ITerminalNode node)
        {
            ASTComposite m_parent = m_parents.Peek();
            Scope currentscope = m_parents_scope.Peek();
            switch (node.Symbol.Type)
            {
                case testlexer.NUMBER:
                    CASTNUMBER newnode1 = new CASTNUMBER(node.Symbol.Text, m_parents.Peek(), m_parents_scope.Peek());
                    m_parent.AddChild(newnode1, m_parentContext.Peek());
                    m_symbolTable.define(newnode1);         // push symbol(inside SymbolTable)
                    break;
                case testlexer.IDENTIFIER:
                    CASTIDENTIFIER newnode2 = new CASTIDENTIFIER(node.Symbol.Text, m_parents.Peek(), m_parents_scope.Peek());
                    m_parent.AddChild(newnode2, m_parentContext.Peek());
                    m_symbolTable.define(newnode2);         // push symbol(inside SymbolTable)
                    break;
                case testlexer.CHAR:
                    CASTCHAR newnode3 = new CASTCHAR(node.Symbol.Text, m_parents.Peek(), m_parents_scope.Peek());
                    m_parent.AddChild(newnode3, m_parentContext.Peek());
                    m_symbolTable.define(newnode3);         // push symbol(inside SymbolTable)
                    break;
                case testlexer.INT_TYPE:
                    CASTINT_TYPE newnode4 = new CASTINT_TYPE(node.Symbol.Text, m_parents.Peek(), m_parents_scope.Peek());
                    m_parent.AddChild(newnode4, m_parentContext.Peek());
                    m_symbolTable.define(newnode4);         // push symbol(inside SymbolTable)
                    break;
                case testlexer.VOID_TYPE:
                    CASTVOID_TYPE newnode5 = new CASTVOID_TYPE(node.Symbol.Text, m_parents.Peek(), m_parents_scope.Peek());
                    m_parent.AddChild(newnode5, m_parentContext.Peek());
                    m_symbolTable.define(newnode5);         // push symbol(inside SymbolTable)
                    break;
                case testlexer.DOUBLE_TYPE:
                    CASTDOUBLE_TYPE newnode7 = new CASTDOUBLE_TYPE(node.Symbol.Text, m_parents.Peek(), m_parents_scope.Peek());
                    m_parent.AddChild(newnode7, m_parentContext.Peek());
                    m_symbolTable.define(newnode7);         // push symbol(inside SymbolTable)
                    break;
                case testlexer.FLOAT_TYPE:
                    CASTFLOAT_TYPE newnode8 = new CASTFLOAT_TYPE(node.Symbol.Text, m_parents.Peek(), m_parents_scope.Peek());
                    m_parent.AddChild(newnode8, m_parentContext.Peek());
                    m_symbolTable.define(newnode8);         // push symbol(inside SymbolTable)
                    break;
                default:
                    break;
            }
            return base.VisitTerminal(node);
        }

    }

}
