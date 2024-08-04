﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.TypeChecker
{
    internal static class TypeMiner
    {
        public static Type TypeMine(ASTElement context)
        {
            Type currentType = null;
            Type previousType = null;

            List<ASTElement> childrenList = context.GetChildrenList();
            if (childrenList == null)
            {
                int nodeType = (int) context.MNodeType;
                switch (nodeType)
                {
                    case (int) C2ASM.nodeType.NT_EXPRESSION_IDENTIFIER:
                        // wait (lookup)
                    break;
                    case (int)C2ASM.nodeType.NT_EXPRESSION_NUMBER:
                        var number = context.M_Text;
                        object resultNumber;

                        if ((resultNumber = Int32.Parse(number))!= null) { // int type
                            return typeof(int);
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
                }
            } else
            {
                foreach (ASTElement child in childrenList)
                {
                    currentType = TypeMine(child);
                    if (previousType != currentType && currentType != null)
                    {
                        currentType = previousType;
                    } else
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