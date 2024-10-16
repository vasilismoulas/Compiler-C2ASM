﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM
{
    class ASTPrinter : ASTBaseVisitor<int,object>
    {
        private static int m_clusterSerial = 0;
        private StreamWriter m_ostream;
        private string m_dotName;

        // Constructor
        public ASTPrinter(string dotFileName)
        {
            m_ostream = new StreamWriter(dotFileName);
            m_dotName = dotFileName;
        }
        /// ###########################################################################|
        /// ExtractSubgraphs ##########################################################|
        /// ###########################################################################|
        /// Αυτή η μέδοδος χωρίζει τα παιδιά σε περιοχές, ανάλογα με τον γωνέα τους ###|
        /// ###########################################################################|
        private void ExtractSubgraphs(ASTComposite node, contextType context)
        {
            Console.WriteLine("Index: " + node.GetContextIndex(context));
            if (node.MChildren[node.GetContextIndex(context)].Count != 0)
            {
                m_ostream.WriteLine("\tsubgraph cluster" + m_clusterSerial++ + "{");
                m_ostream.WriteLine("\t\tnode [style=filled,color=white];");
                m_ostream.WriteLine("\t\tstyle=filled;");
                m_ostream.WriteLine("\t\tcolor=lightgrey;");
                m_ostream.Write("\t\t");
                for (int i = 0; i < node.MChildren[node.GetContextIndex(context)].Count; i++)
                {
                    m_ostream.Write(node.MChildren[node.GetContextIndex(context)][i].MNodeName + ";");
                }

                m_ostream.WriteLine("\n\t\tlabel=" + context + ";");
                m_ostream.WriteLine("\t}");
            }
        }

        public override int VisitCOMPILEUNIT(CASTCompileUnit node,object param)
        {

            m_ostream.WriteLine("digraph {");

            ExtractSubgraphs(node, contextType.CT_COMPILEUNIT_FUNCTIONDEFINITION);
            ExtractSubgraphs(node, contextType.CT_COMPILEUNIT_GLOBALSTATEMENT);

            base.VisitCOMPILEUNIT(node);

            m_ostream.WriteLine("}");
            m_ostream.Close();

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif ASyntaxTree.dot  -o ASyntaxTree.gif";
            // Enter the executable to run, including the complete path
            start.FileName = "dot";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

            return 0;
        }

        public override int VisitIDENTIFIER(CASTIDENTIFIER node,object param)
        {
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return base.VisitIDENTIFIER(node);
        }

        public override int VisitFCALL(CASTExpressionFCALL node, object param = default(object)) {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_FCALLNAME);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_FCALLARGS);
            base.VisitFCALL(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitNUMBER(CASTNUMBER node, object param)
        {
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return base.VisitNUMBER(node);
        }

        public override int VisitAddition(CASTExpressionAddition node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_ADDITION_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_ADDITION_RIGHT);
            base.VisitAddition(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitSubtraction(CASTExpressionSubtraction node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_SUBTRACTION_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_SUBTRACTION_RIGHT);
            base.VisitSubtraction(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitMultiplication(CASTExpressionMultiplication node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_MULTIPLICATION_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_MULTIPLICATION_RIGHT);
            base.VisitMultiplication(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitDivision(CASTExpressionDivision node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_DIVISION_LEFT);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_DIVISION_RIGHT);
            base.VisitDivision(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitASSIGN(CASTExpressionAssign node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_ASSIGN_LVALUE);
            ExtractSubgraphs(node, contextType.CT_EXPRESSION_ASSIGN_EXPRESSION);
            base.VisitASSIGN(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }
        public override int VisitSTATEMENT(CASTStatement node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_STATEMENT);
            base.VisitSTATEMENT(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0; 
        }

        public override int VisitFunctionDefinition(CASTFunctionDefinition node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_FUNCTIONDEFINITION_FUNPREFIX);
            ExtractSubgraphs(node, contextType.CT_FUNCTIONDEFINITION_FARGUMENTS);
            ExtractSubgraphs(node, contextType.CT_FUNCTIONDEFINITION_BODY);
            base.VisitFunctionDefinition(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitFormalArgs(CASTFormalArgs node, object param) {
            ExtractSubgraphs(node, contextType.CT_FARGUMENTS_DATADECLARATION);
            base.VisitFormalArgs(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitDATADECLARATION(CASTDatadeclaration node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_DATADECLARATION_TYPESPECIFIER);
            ExtractSubgraphs(node, contextType.CT_DATADECLARATION_IDENTIFIER);
            ExtractSubgraphs(node, contextType.CT_DATADECLARATION_DATAVALUE);
            base.VisitDATADECLARATION(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitFunctionDeclaration(CASTFunctionDeclaration node, object param) {
            ExtractSubgraphs(node, contextType.CT_FUNCTIONDECLARATION_FUNPREFIX);
            ExtractSubgraphs(node, contextType.CT_FUNCTIONDECLARATION_FARGUMENTS);
            base.VisitFunctionDeclaration(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitFUNCTIONBODY(CASTFunctionBody node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_FUNCTIONBODY_STATEMENT);
            base.VisitFUNCTIONBODY(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
            return 0;
        }

        public override int VisitFunprefix(CASTFunprefix node, object param)
        {
            ExtractSubgraphs(node, contextType.CT_FUNPREFIX_TYPESPECIFIER);
            ExtractSubgraphs(node, contextType.CT_FUNPREFIX_IDENTIFIER);
            base.VisitFunprefix(node);
            m_ostream.WriteLine("{0}->{1}", node.MParent.MNodeName, node.MNodeName);
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

        public override int VisitSTATEMENTBREAK(CASTBreakStatement node, object param = default(object)) {
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

        public override int VisitParenthesis(CASTExpressionInParenthesis node, object param = default(object)) {
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