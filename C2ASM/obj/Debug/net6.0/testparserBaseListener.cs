//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/silve/source/comp-2-repos/Compiler-C2ASM/C2ASM/testparser.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419


using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ItestparserListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
public partial class testparserBaseListener : ItestparserListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.compileUnit"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompileUnit([NotNull] testparser.CompileUnitContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.compileUnit"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompileUnit([NotNull] testparser.CompileUnitContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.globalstatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGlobalstatement([NotNull] testparser.GlobalstatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.globalstatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGlobalstatement([NotNull] testparser.GlobalstatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.functionDeclaration"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunctionDeclaration([NotNull] testparser.FunctionDeclarationContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.functionDeclaration"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunctionDeclaration([NotNull] testparser.FunctionDeclarationContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>custom_FunctionDefinition</c>
	/// labeled alternative in <see cref="testparser.functionDefinition"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCustom_FunctionDefinition([NotNull] testparser.Custom_FunctionDefinitionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>custom_FunctionDefinition</c>
	/// labeled alternative in <see cref="testparser.functionDefinition"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCustom_FunctionDefinition([NotNull] testparser.Custom_FunctionDefinitionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.funprefix"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunprefix([NotNull] testparser.FunprefixContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.funprefix"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunprefix([NotNull] testparser.FunprefixContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.functionbody"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunctionbody([NotNull] testparser.FunctionbodyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.functionbody"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunctionbody([NotNull] testparser.FunctionbodyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>statement_ExpressionStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement_ExpressionStatement([NotNull] testparser.Statement_ExpressionStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>statement_ExpressionStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement_ExpressionStatement([NotNull] testparser.Statement_ExpressionStatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>statement_IfStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement_IfStatement([NotNull] testparser.Statement_IfStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>statement_IfStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement_IfStatement([NotNull] testparser.Statement_IfStatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>statement_WhileStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement_WhileStatement([NotNull] testparser.Statement_WhileStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>statement_WhileStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement_WhileStatement([NotNull] testparser.Statement_WhileStatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>statement_CompoundStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement_CompoundStatement([NotNull] testparser.Statement_CompoundStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>statement_CompoundStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement_CompoundStatement([NotNull] testparser.Statement_CompoundStatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>statement_DataDeclarationStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement_DataDeclarationStatement([NotNull] testparser.Statement_DataDeclarationStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>statement_DataDeclarationStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement_DataDeclarationStatement([NotNull] testparser.Statement_DataDeclarationStatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>statement_ReturnStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement_ReturnStatement([NotNull] testparser.Statement_ReturnStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>statement_ReturnStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement_ReturnStatement([NotNull] testparser.Statement_ReturnStatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>statement_BreakStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement_BreakStatement([NotNull] testparser.Statement_BreakStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>statement_BreakStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement_BreakStatement([NotNull] testparser.Statement_BreakStatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.ifstatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIfstatement([NotNull] testparser.IfstatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.ifstatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIfstatement([NotNull] testparser.IfstatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.whilestatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWhilestatement([NotNull] testparser.WhilestatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.whilestatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWhilestatement([NotNull] testparser.WhilestatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.compoundStatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompoundStatement([NotNull] testparser.CompoundStatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.compoundStatement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompoundStatement([NotNull] testparser.CompoundStatementContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.statementList"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatementList([NotNull] testparser.StatementListContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.statementList"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatementList([NotNull] testparser.StatementListContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.datadeclaration"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDatadeclaration([NotNull] testparser.DatadeclarationContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.datadeclaration"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDatadeclaration([NotNull] testparser.DatadeclarationContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>datavalue_Number</c>
	/// labeled alternative in <see cref="testparser.datavalue"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDatavalue_Number([NotNull] testparser.Datavalue_NumberContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>datavalue_Number</c>
	/// labeled alternative in <see cref="testparser.datavalue"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDatavalue_Number([NotNull] testparser.Datavalue_NumberContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>datavalue_Char</c>
	/// labeled alternative in <see cref="testparser.datavalue"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDatavalue_Char([NotNull] testparser.Datavalue_CharContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>datavalue_Char</c>
	/// labeled alternative in <see cref="testparser.datavalue"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDatavalue_Char([NotNull] testparser.Datavalue_CharContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>typespecifier_IntType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTypespecifier_IntType([NotNull] testparser.Typespecifier_IntTypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>typespecifier_IntType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTypespecifier_IntType([NotNull] testparser.Typespecifier_IntTypeContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>typespecifier_DoubleType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTypespecifier_DoubleType([NotNull] testparser.Typespecifier_DoubleTypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>typespecifier_DoubleType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTypespecifier_DoubleType([NotNull] testparser.Typespecifier_DoubleTypeContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>typespecifier_FloatType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTypespecifier_FloatType([NotNull] testparser.Typespecifier_FloatTypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>typespecifier_FloatType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTypespecifier_FloatType([NotNull] testparser.Typespecifier_FloatTypeContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>typespecifier_CharType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTypespecifier_CharType([NotNull] testparser.Typespecifier_CharTypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>typespecifier_CharType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTypespecifier_CharType([NotNull] testparser.Typespecifier_CharTypeContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>typespecifier_VoidType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTypespecifier_VoidType([NotNull] testparser.Typespecifier_VoidTypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>typespecifier_VoidType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTypespecifier_VoidType([NotNull] testparser.Typespecifier_VoidTypeContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_PLUSMINUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_PLUSMINUS([NotNull] testparser.Expr_PLUSMINUSContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_PLUSMINUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_PLUSMINUS([NotNull] testparser.Expr_PLUSMINUSContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_LTE</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_LTE([NotNull] testparser.Expr_LTEContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_LTE</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_LTE([NotNull] testparser.Expr_LTEContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_ASSIGN</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_ASSIGN([NotNull] testparser.Expr_ASSIGNContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_ASSIGN</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_ASSIGN([NotNull] testparser.Expr_ASSIGNContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_LT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_LT([NotNull] testparser.Expr_LTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_LT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_LT([NotNull] testparser.Expr_LTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_CHAR</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_CHAR([NotNull] testparser.Expr_CHARContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_CHAR</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_CHAR([NotNull] testparser.Expr_CHARContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_NUMBER</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_NUMBER([NotNull] testparser.Expr_NUMBERContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_NUMBER</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_NUMBER([NotNull] testparser.Expr_NUMBERContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_NEQUAL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_NEQUAL([NotNull] testparser.Expr_NEQUALContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_NEQUAL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_NEQUAL([NotNull] testparser.Expr_NEQUALContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_PLUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_PLUS([NotNull] testparser.Expr_PLUSContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_PLUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_PLUS([NotNull] testparser.Expr_PLUSContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_EQUAL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_EQUAL([NotNull] testparser.Expr_EQUALContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_EQUAL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_EQUAL([NotNull] testparser.Expr_EQUALContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_GT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_GT([NotNull] testparser.Expr_GTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_GT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_GT([NotNull] testparser.Expr_GTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_MULDIV</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_MULDIV([NotNull] testparser.Expr_MULDIVContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_MULDIV</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_MULDIV([NotNull] testparser.Expr_MULDIVContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_IDENTIFIER</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_IDENTIFIER([NotNull] testparser.Expr_IDENTIFIERContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_IDENTIFIER</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_IDENTIFIER([NotNull] testparser.Expr_IDENTIFIERContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_MINUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_MINUS([NotNull] testparser.Expr_MINUSContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_MINUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_MINUS([NotNull] testparser.Expr_MINUSContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_FCALL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_FCALL([NotNull] testparser.Expr_FCALLContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_FCALL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_FCALL([NotNull] testparser.Expr_FCALLContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_OR</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_OR([NotNull] testparser.Expr_ORContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_OR</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_OR([NotNull] testparser.Expr_ORContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_PAREN</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_PAREN([NotNull] testparser.Expr_PARENContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_PAREN</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_PAREN([NotNull] testparser.Expr_PARENContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_NOT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_NOT([NotNull] testparser.Expr_NOTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_NOT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_NOT([NotNull] testparser.Expr_NOTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_GTE</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_GTE([NotNull] testparser.Expr_GTEContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_GTE</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_GTE([NotNull] testparser.Expr_GTEContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>expr_AND</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr_AND([NotNull] testparser.Expr_ANDContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>expr_AND</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr_AND([NotNull] testparser.Expr_ANDContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.args"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterArgs([NotNull] testparser.ArgsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.args"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitArgs([NotNull] testparser.ArgsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="testparser.formalargs"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFormalargs([NotNull] testparser.FormalargsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="testparser.formalargs"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFormalargs([NotNull] testparser.FormalargsContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
