using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.TypeCheck
{
    internal static class TypeMiner
    {

        public static Type TypeMine(ASTElement context, testparser parser)
        {
            Type currentType = null;
            Type previousType = null;

            List<ASTElement>[] childrenList = context.GetChildrenList();
            if (childrenList == null)
            {
                int nodeType = (int) context.MNodeType;
                switch (nodeType)
                {
                    case (int) C2ASM.nodeType.NT_EXPRESSION_IDENTIFIER:
                        ASTElement returnvalue = parser.symtab.GetElement(context, context.M_Text.Replace("\"", ""), context.GetElementScope());
                        if(returnvalue != null)
                        {
                            return returnvalue.m_type;
                        } else
                        {
                            return null;
                        }
                    case (int)C2ASM.nodeType.NT_EXPRESSION_NUMBER:
                        var number = context.M_Text;
                        object resultNumber;

                        if ((resultNumber = Int32.Parse(number))!= null) { // int type
                            return typeof(int);         // NOTE: kim C# autoboxing
                        } else if ((resultNumber = Double.Parse(number)) != null) { // double type
                            return typeof(double);
                        } else if ((resultNumber = Int32.Parse(number)) != null) { // float type
                            return typeof(float);
                        } else {
                            return null;
                        }
                    case (int)C2ASM.nodeType.NT_EXPRESSION_CHAR:
                        return typeof(char);
                    case (int)C2ASM.nodeType.NT_VOID_TYPE:
                        return typeof(void);
                    case (int)C2ASM.nodeType.NT_INT_TYPE:
                        return typeof(int);
                    case (int)C2ASM.nodeType.NT_DOUBLE_TYPE:
                        return typeof(double);
                    case (int)C2ASM.nodeType.NT_FLOAT_TYPE:
                        return typeof(float);
                    case (int)C2ASM.nodeType.NT_CHAR_TYPE:
                        return typeof(char);
                }
            } else
            {
                bool firstIteration = true;
                foreach (List<ASTElement> childNode in childrenList)
                {
                    if (childNode.Count == 0) {
                        continue;
                    }
                    ASTElement child = childNode[0];
                    currentType = TypeMine(child, parser);
                    if (firstIteration)
                    {
                        firstIteration = false;
                        previousType = currentType;
                    }
                    else if (previousType == null || currentType == null || currentType != previousType)
                    {
                        currentType = null;
                        break;
                    }
                }
            }
            return currentType; //Line to be removed
        }
    }
}
