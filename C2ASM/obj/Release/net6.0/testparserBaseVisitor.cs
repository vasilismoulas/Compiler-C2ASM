//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/vasil/OneDrive/Έγγραφα/test/Compiler-C2ASM/C2ASM/testparser.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ItestparserVisitor{Result}"/>,
/// which can be extended to create a visitor which only needs to handle a subset
/// of the available methods.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
public partial class testparserBaseVisitor<Result> : AbstractParseTreeVisitor<Result>, ItestparserVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.compileUnit"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCompileUnit([NotNull] testparser.CompileUnitContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.globalstatement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitGlobalstatement([NotNull] testparser.GlobalstatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.functionDeclaration"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFunctionDeclaration([NotNull] testparser.FunctionDeclarationContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>custom_FunctionDefinition</c>
	/// labeled alternative in <see cref="testparser.functionDefinition"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCustom_FunctionDefinition([NotNull] testparser.Custom_FunctionDefinitionContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.funprefix"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFunprefix([NotNull] testparser.FunprefixContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.functionbody"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFunctionbody([NotNull] testparser.FunctionbodyContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>statement_ExpressionStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStatement_ExpressionStatement([NotNull] testparser.Statement_ExpressionStatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>statement_IfStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStatement_IfStatement([NotNull] testparser.Statement_IfStatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>statement_WhileStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStatement_WhileStatement([NotNull] testparser.Statement_WhileStatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>statement_CompoundStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStatement_CompoundStatement([NotNull] testparser.Statement_CompoundStatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>statement_DataDeclarationStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStatement_DataDeclarationStatement([NotNull] testparser.Statement_DataDeclarationStatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>statement_ReturnStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStatement_ReturnStatement([NotNull] testparser.Statement_ReturnStatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>statement_BreakStatement</c>
	/// labeled alternative in <see cref="testparser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStatement_BreakStatement([NotNull] testparser.Statement_BreakStatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.ifstatement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitIfstatement([NotNull] testparser.IfstatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.whilestatement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitWhilestatement([NotNull] testparser.WhilestatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.compoundStatement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCompoundStatement([NotNull] testparser.CompoundStatementContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.statementList"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStatementList([NotNull] testparser.StatementListContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.datadeclaration"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitDatadeclaration([NotNull] testparser.DatadeclarationContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>datavalue_Number</c>
	/// labeled alternative in <see cref="testparser.datavalue"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitDatavalue_Number([NotNull] testparser.Datavalue_NumberContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>datavalue_Char</c>
	/// labeled alternative in <see cref="testparser.datavalue"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitDatavalue_Char([NotNull] testparser.Datavalue_CharContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>typespecifier_IntType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitTypespecifier_IntType([NotNull] testparser.Typespecifier_IntTypeContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>typespecifier_DoubleType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitTypespecifier_DoubleType([NotNull] testparser.Typespecifier_DoubleTypeContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>typespecifier_FloatType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitTypespecifier_FloatType([NotNull] testparser.Typespecifier_FloatTypeContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>typespecifier_CharType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitTypespecifier_CharType([NotNull] testparser.Typespecifier_CharTypeContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>typespecifier_VoidType</c>
	/// labeled alternative in <see cref="testparser.typespecifier"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitTypespecifier_VoidType([NotNull] testparser.Typespecifier_VoidTypeContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_PLUSMINUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_PLUSMINUS([NotNull] testparser.Expr_PLUSMINUSContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_LTE</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_LTE([NotNull] testparser.Expr_LTEContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_ASSIGN</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_ASSIGN([NotNull] testparser.Expr_ASSIGNContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_LT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_LT([NotNull] testparser.Expr_LTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_CHAR</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_CHAR([NotNull] testparser.Expr_CHARContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_NUMBER</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_NUMBER([NotNull] testparser.Expr_NUMBERContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_NEQUAL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_NEQUAL([NotNull] testparser.Expr_NEQUALContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_PLUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_PLUS([NotNull] testparser.Expr_PLUSContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_EQUAL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_EQUAL([NotNull] testparser.Expr_EQUALContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_GT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_GT([NotNull] testparser.Expr_GTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_MULDIV</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_MULDIV([NotNull] testparser.Expr_MULDIVContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_IDENTIFIER</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_IDENTIFIER([NotNull] testparser.Expr_IDENTIFIERContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_MINUS</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_MINUS([NotNull] testparser.Expr_MINUSContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_FCALL</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_FCALL([NotNull] testparser.Expr_FCALLContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_OR</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_OR([NotNull] testparser.Expr_ORContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_PAREN</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_PAREN([NotNull] testparser.Expr_PARENContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_NOT</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_NOT([NotNull] testparser.Expr_NOTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_GTE</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_GTE([NotNull] testparser.Expr_GTEContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>expr_AND</c>
	/// labeled alternative in <see cref="testparser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExpr_AND([NotNull] testparser.Expr_ANDContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.args"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitArgs([NotNull] testparser.ArgsContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="testparser.formalargs"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFormalargs([NotNull] testparser.FormalargsContext context) { return VisitChildren(context); }
}
