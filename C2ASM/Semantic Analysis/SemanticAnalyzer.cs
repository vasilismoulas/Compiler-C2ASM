using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.Semantic_Analysis
{
    public class SemanticAnalyzer
    {
        private ASTElement root;
        private bool mainFunctionFound;

        public SemanticAnalyzer(ASTElement root)
        {
            this.root = root;
            this.mainFunctionFound = false;
        }

        //public void Analyze()
        //{
        //    TraverseAST(root);

        //    if (!mainFunctionFound)
        //    {
        //        throw new SemanticException("No 'main' function found.");
        //    }
        //}

        //private void TraverseAST(ASTElement node)
        //{
        //    // Traverse the AST nodes
        //    foreach (var child in node.Children)
        //    {
        //        if (child is FunctionDeclarationNode functionNode)
        //        {
        //            if (functionNode.Name == "main" && IsValidMainFunction(functionNode))
        //            {
        //                mainFunctionFound = true;
        //            }
        //        }

        //        TraverseAST(child);
        //    }
        //}

        //private bool IsValidMainFunction(FunctionDeclarationNode functionNode)
        //{
        //    // Check the signature of the main function
        //    return functionNode.ReturnType == "int" &&
        //           (functionNode.Parameters.Count == 0 ||
        //            (functionNode.Parameters.Count == 2 &&
        //             functionNode.Parameters[0].Type == "int" &&
        //             functionNode.Parameters[1].Type == "char**"));
        //}
    }
}
