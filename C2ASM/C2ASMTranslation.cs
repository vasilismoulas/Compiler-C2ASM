//using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using C_ASM;
using C2ASM;
using Xunit.Sdk;

namespace C2ASM
{
    public class TranslationParameters
    {
        // Provides access to the ancestor container object. Provides 
        // access to properties of the parent during construction of the
        // current element
        private CEmmitableCodeContainer m_parent = null;
        // Provides access to the FunctionDefinition that
        // hosts the current element. Provides access to the API
        // of the container function i.e to Declare a variable etc
        private CCFunctionDefinition m_containerFunction = null;
        // Provides the context of the parent container object
        // where the current container shouldbe placed. Necessary 
        // when the current object is created and placed in the parent
        // at a specific context
        private CodeContextType m_parentContextType = CodeContextType.CC_NA;

        // below are extra stuff silver included to make this betty work

        // Provides access to the function definition element that hosts the current element.
        private CASTFunctionDefinition m_functionParent = null;
        // Provides access to the while loop element that hosts the current element.
        private CASTWhileStatement m_loopParent = null;
        // Provides access to the if conditional element that hosts the current element.
        private CASTIfStatement m_conditionalParent = null;
        // Provides information about whether a conditional comparison will be translated 
        // for if conditional or while loop use.
        private String m_conditionalCase = "";




        public TranslationParameters()
        {

        }

        public TranslationParameters(CEmmitableCodeContainer m_parent, CCFunctionDefinition m_containerFunction, CodeContextType m_parentContextType, CASTWhileStatement m_loopParent, CASTIfStatement m_conditionalParent, String m_conditionalCase)
        {
            this.m_parent = m_parent;
            this.m_containerFunction = m_containerFunction;
            this.m_parentContextType = m_parentContextType;
            this.m_loopParent = m_loopParent;
            this.m_conditionalParent = m_conditionalParent;
            this.m_conditionalCase = m_conditionalCase;
        }

        public CEmmitableCodeContainer M_Parent
        {
            get => m_parent;
            set => m_parent = value;
        }
        public CodeContextType M_ParentContextType
        {
            get => m_parentContextType;
            set => m_parentContextType = value;
        }

        public CCFunctionDefinition M_ContainerFunction
        {
            get => m_containerFunction;
            set => m_containerFunction = value;
        }
        public CASTFunctionDefinition M_FunctionParent
        {
            get => m_functionParent;
            set => m_functionParent = value;
        }
        public CASTWhileStatement M_LoopParent
        {
            get => m_loopParent;
            set => m_loopParent = value;
        }
        public CASTIfStatement M_ConditionalParent
        {
            get => m_conditionalParent;
            set => m_conditionalParent = value;
        }
        public String M_ConditionalCase
        {
            get => m_conditionalCase;
            set => m_conditionalCase = value;
        }
    }

    class C2ASMTranslation : ASTBaseVisitor<CEmmitableCodeContainer, TranslationParameters>
    {
        private CCFile m_translatedFile;

        public CCFile M_TranslatedFile => m_translatedFile;

        private testparser m_testparser;

        public testparser M_Testparser => m_testparser;

        public C2ASMTranslation(testparser parser)
        {
            m_testparser = parser;
        }


        public override CEmmitableCodeContainer VisitCOMPILEUNIT(CASTCompileUnit node, TranslationParameters param)
        {
            //CCFunctionDefinition mainf;
            //1. Create Output File
            m_translatedFile = new CCFile(false);
            //mainf = m_translatedFile.MainDefinition;

            m_translatedFile.AddCode(".model medium \n", CodeContextType.CC_FILE_PREPROCESSOR);
            m_translatedFile.AddCode(".stack 100h \n", CodeContextType.CC_FILE_PREPROCESSOR);
            m_translatedFile.AddCode(".data \n", CodeContextType.CC_FILE_PREPROCESSOR);
            m_translatedFile.AddCode(".code \n", CodeContextType.CC_FILE_PREPROCESSOR);

            //// 2. Visit CT_COMPILEUNIT_GLOBALSTATEMENT (according to our grammar rules, it refers only to function declarations)
            //VisitContext(node, contextType.CT_COMPILEUNIT_GLOBALSTATEMENT,
            //    new TranslationParameters()
            //    {
            //        M_Parent = mainf.GetChild(CodeContextType.CC_FUNCTIONDEFINITION_BODY),
            //        M_ContainerFunction = mainf,
            //        M_ParentContextType = CodeContextType.CC_COMPOUNDSTATEMENT_BODY
            //    });

            // 3. Visit CT_COMPILEUNIT_FUNCTIONDEFINITIONS
            VisitContext(node, contextType.CT_COMPILEUNIT_FUNCTIONDEFINITION,
                new TranslationParameters()
                {
                    M_Parent = m_translatedFile,
                    M_ParentContextType = CodeContextType.CC_FILE_FUNDEF
                });

            //3. Visit CT
            return m_translatedFile;
        }

        public override CEmmitableCodeContainer VisitFunctionDefinition(CASTFunctionDefinition node, TranslationParameters param)
        {
            //1. Create a Function Definition code context
            CCFunctionDefinition fundef = new CCFunctionDefinition(param.M_Parent);

            //2. Add Function Definition to the File in the appropriate context
            param.M_Parent.AddCode(fundef, param.M_ParentContextType);

            //3. Assemble the function header
            CASTFunprefix funprefixthing = node.GetChild(contextType.CT_FUNCTIONDEFINITION_FUNPREFIX, 0) as CASTFunprefix;
            CASTIDENTIFIER id = funprefixthing.GetChild(contextType.CT_FUNPREFIX_IDENTIFIER, 0) as CASTIDENTIFIER;

            fundef.EnterScope();

            fundef.AddCode(id.M_Text + " PROC" + "\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            fundef.AddCode("\tpush ebp" + "\n"
                            + "mov ebp,esp" + "\n"
                            + "push eax" + "\n"
                            + "push ebx" + "\n"
                            + "push ecx" + "\n"
                            + "push edx" + "\n"
                            + "push esi" + "\n"
                            + "push edi" + "\n" + "\n"
                            , CodeContextType.CC_FUNCTIONDEFINITION_HEADER);

            // add argument variables
            fundef.AddCode("\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            fundef.AddCode("\t;Arguments:\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);

            // FUNARGUMENTS
            if (node.ChildExists(contextType.CT_FUNCTIONDEFINITION_FARGUMENTS, 0) != null)
            {
                //foreach(string identifier in node.GetFunctionArgs())
                //{
                // fundef.Addcode("\t\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER) for each function's argument...
                //}
            }


            // add inside code visitor
            fundef.AddCode("\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            fundef.AddCode("\t;Function body:\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);

            if (node.ChildExists(contextType.CT_FUNCTIONDEFINITION_BODY, 0) != null)
            {
            CEmmitableCodeContainer visitValue = Visit(node.GetContextChildren(contextType.CT_FUNCTIONDEFINITION_BODY)[0], new TranslationParameters()
                {
                    M_ContainerFunction = fundef,
                    M_ParentContextType = CodeContextType.CC_FUNCTIONDEFINITION_BODY,
                    M_Parent = fundef
            });

            fundef.AddCode(visitValue, CodeContextType.CC_FUNCTIONDEFINITION_BODY);
            }
                

            fundef.AddCode("\n" + id.M_Text + "END:" + "\n"
                            + "pop edi" + "\n"
                            + "pop esi" + "\n"
                            + "pop eax" + "\n"
                            + "pop edx" + "\n"
                            + "pop ecx" + "\n"
                            + "pop ebx" + "\n"
                            + "mov esp,ebp" + "\n"
                            + "pop ebp" + "\n"
                            + "ret" + "\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);

            fundef.LeaveScope();
            fundef.AddCode(id.M_Text + "ENDP" + "\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);


            return fundef;
        }

        public override CEmmitableCodeContainer VisitFunctionDeclaration(CASTFunctionDeclaration node, TranslationParameters param) 
        { 
            CCFunctionDefinition rep = new CCFunctionDefinition(M_TranslatedFile);
            M_TranslatedFile.AddCode(node.GetDeclarationName() + " PROTO\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            return rep;
        }

        //public CEmmitableCodeContainer VisitDataDeclaration(CASTDatadeclaration node, TranslationParameters param)
        //{
        //    CCFunctionDefinition rep = new CCFunctionDefinition(param.M_Parent);

        //    Type type = node.GetContextChildren(contextType.CT_DATADECLARATION_TYPESPECIFIER)[0].GetChildrenList()[0].GetType();
        //    ASTElement identifier = node.GetContextChildren(contextType.CT_DATADECLARATION_IDENTIFIER)[0];
        //    ASTElement datavalue = node.GetContextChildren(contextType.CT_DATADECLARATION_DATAVALUE)[0];

        //    String typeString = "", identifierString = "", datavalueString = "";

        //    // we need to find the type first
        //    if (type.Equals(typeof(CASTCHAR_TYPE)) || type.Equals(typeof(CASTVOID_TYPE)))
        //    {
        //        typeString = "SBYTE";
        //    }
        //    else if (type.Equals(typeof(CASTINT_TYPE)))
        //    {
        //        typeString = "SWORD";
        //    }
        //    else if (type.Equals(typeof(CASTDOUBLE_TYPE)) || type.Equals(typeof(CASTFLOAT_TYPE)))
        //    {
        //        typeString = "SDWORD";
        //    }


        //    identifierString = identifier.m_name_text;

        //    if(datavalue == null)
        //    {
        //        typeString = "?";
        //    } else
        //    {
        //        typeString = datavalue.m_name_text;
        //    }

        //    rep.AddCode(datavalueString + " " + identifierString + " " + datavalue + "\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
        //    return rep;
        //}

        public override CEmmitableCodeContainer VisitASSIGN(CASTExpressionAssign node, TranslationParameters param = default(TranslationParameters))
        {
            CCFunctionDefinition fun = param.M_ContainerFunction as CCFunctionDefinition;

            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, M_TranslatedFile);

            ASTElement assignLeftPart = node.GetChild(contextType.CT_EXPRESSION_ASSIGN_LVALUE, 0);
            ASTElement assignRightPart = node.GetChild(contextType.CT_EXPRESSION_ASSIGN_EXPRESSION, 0);

            //ASTElement assignLeftPart = node.GetChildrenList()[0][0];
            //ASTElement assignRightPart = node.GetChildrenList()[1][0];

            if (assignRightPart.GetType().Equals(typeof(CASTIDENTIFIER)) || assignRightPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode(assignLeftPart.m_name_text + assignRightPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(assignRightPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov " + assignLeftPart.m_name_text + ",eax\n");
                M_TranslatedFile.AddCode("asdasdasdasd", CodeContextType.CC_NA);
            }



            //param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            //CASTIDENTIFIER id = node.GetChild(contextType.CT_EXPRESSION_ASSIGN_LVALUE, 0) as CASTIDENTIFIER;
            //fun.DeclareVariable(id.M_Text, false);
            //rep.AddCode(id.M_Text);
            //rep.AddCode("=");
            //rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_ASSIGN_EXPRESSION, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_Parent = null,
            //    M_ParentContextType = CodeContextType.CC_NA
            //}).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitFCALL(CASTExpressionFCALL node,
            TranslationParameters param = default(TranslationParameters))
        {
            CCFunctionDefinition fun = param.M_ContainerFunction as CCFunctionDefinition;
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);

            int argumentTotalSize = 0;

            // How do we get the correct functionDefinition ??
            // Maybe by using the symbol table from the test parser.
            CASTFunctionDefinition fundef = (CASTFunctionDefinition) m_testparser.symtab.resolve(node.m_name_text);

            // Get arguments
            CASTFormalArgs formalArgumentList = (CASTFormalArgs) fundef.GetChild(contextType.CT_FUNCTIONDECLARATION_FARGUMENTS, 0);

            // Push every argument
            for(int i = 0; i < formalArgumentList.GetChildrenList().Length; i++)
            {
                // Getting formal argument
                CASTDatadeclaration argument = (CASTDatadeclaration) formalArgumentList.GetChildrenList()[i].First();

                // Getting argument identifier
                String argumentIdentifier = argument.GetChildrenList()[1].First().m_name_text;
                // Pushing formal argument
                rep.AddCode("push " + argumentIdentifier + "\n");
            }

            // call function
            rep.AddCode("call " + fundef.GetFunctionName() + "\n");

            // revert esp back to its original value (F CALL)
            argumentTotalSize *= 4;
            rep.AddCode("add esp," + argumentTotalSize + "\n");


            //CASTIDENTIFIER id = node.GetChild(contextType.CT_EXPRESSION_FCALLNAME, 0) as CASTIDENTIFIER;
            //string funheader = "float " + id.M_Text + "(";
            //for (int i = 0; i < node.MChildren[1].Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        funheader += ",";
            //    }
            //    funheader += "float x" + i;
            //}
            //funheader += ")";
            //m_translatedFile.DeclareFunction(id.M_Text, funheader);

            //rep.AddCode(id.M_Text);
            //rep.AddCode("(");
            //for (int i = 0; i < node.MChildren[1].Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        rep.AddCode(",");
            //    }
            //    rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_FCALLARGS, i), new TranslationParameters()
            //    {
            //        M_ContainerFunction = param.M_ContainerFunction,
            //        M_Parent = null,
            //        M_ParentContextType = CodeContextType.CC_NA
            //    }).AssemblyCodeContainer());
            //}

            //rep.AddCode(")");
            return rep;
        }

        public override CEmmitableCodeContainer VisitAddition(CASTExpressionAddition node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode("\n");
            rep.AddCode("push ebx" + "\n" +
                        "push ecx" + "\n\n");

            //if first value not identifier or value, expand assignment of first value to ebx
            ASTElement additionLeftPart = node.GetChild(contextType.CT_EXPRESSION_ADDITION_LEFT, 0);

            if (additionLeftPart.GetType().Equals(typeof(CASTIDENTIFIER)) || additionLeftPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov ebx," + additionLeftPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(additionLeftPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ebx,eax\n");
            }

            //if second value not identifier or value, expand assignment of second value to ecx
            ASTElement additionRightPart = node.GetChild(contextType.CT_EXPRESSION_ADDITION_LEFT, 0);
            if (additionRightPart.GetType().Equals(typeof(CASTIDENTIFIER)) || additionRightPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov ecx," + additionRightPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(additionRightPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ecx,eax\n");
            }


            rep.AddCode("\n" + "mov eax,ebx" + 
                        "\n" + "add eax,ecx" + 
                        "\n" + "pop ebx" +
                        "\n" + "pop ecx");


            // Junk code
            //ASTElement ele2 = node.GetChildrenList()[1][0];
            //rep.AddCode("+");
            //rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_ADDITION_RIGHT, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_Parent = null,
            //    M_ParentContextType = CodeContextType.CC_NA
            //}).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitSubtraction(CASTExpressionSubtraction node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode("\n");
            rep.AddCode("push ebx" + "\n" +
                        "push ecx" + "\n\n");

            //if first value not identifier or value, expand assignment of first value to ebx
            ASTElement subtractionLeftPart = node.GetChild(contextType.CT_EXPRESSION_SUBTRACTION_LEFT, 0);

            if (subtractionLeftPart.GetType().Equals(typeof(CASTIDENTIFIER)) || subtractionLeftPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov ebx," + subtractionLeftPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(subtractionLeftPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ebx,eax\n");
            }
            //if second value not identifier or value, expand assignment of second value to ecx
            ASTElement subtractionRightPart = node.GetChild(contextType.CT_EXPRESSION_SUBTRACTION_RIGHT, 0);
            if (subtractionRightPart.GetType().Equals(typeof(CASTIDENTIFIER)) || subtractionRightPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov ecx," + subtractionRightPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(subtractionRightPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ecx,eax\n");
            }


            rep.AddCode("\n" + "mov eax,ebx" +
                        "\n" + "mul eax,ecx" +
                        "\n" + "pop ebx" +
                        "\n" + "pop ecx");
            return rep;
        }

        public override CEmmitableCodeContainer VisitMultiplication(CASTExpressionMultiplication node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode("\n");
            rep.AddCode("push ebx" + "\n" +
                        "push ecx" + "\n\n");

            //if first value not identifier or value, expand assignment of first value to ebx
            ASTElement multiplicationLeftPart = node.GetChild(contextType.CT_EXPRESSION_MULTIPLICATION_LEFT, 0);

            if (multiplicationLeftPart.GetType().Equals(typeof(CASTIDENTIFIER)) || multiplicationLeftPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov ebx," + multiplicationLeftPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(multiplicationLeftPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ebx,eax\n");
            }

            //if second value not identifier or value, expand assignment of second value to ecx
            ASTElement multiplicationRightPart = node.GetChild(contextType.CT_EXPRESSION_MULTIPLICATION_RIGHT, 0);
            if (multiplicationRightPart.GetType().Equals(typeof(CASTIDENTIFIER)) || multiplicationRightPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov ecx," + multiplicationRightPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(multiplicationRightPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ecx,eax\n");
            }


            rep.AddCode("\n" + "mov eax,ebx" +
                        "\n" + "mul eax,ecx" +
                        "\n" + "pop ebx" +
                        "\n" + "pop ecx");
            return rep;
        }

        public override CEmmitableCodeContainer VisitDivision(CASTExpressionDivision node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode("\n");
            rep.AddCode("push ebx" + "\n" +
                        "push ecx" + "\n\n");

            //if first value not identifier or value, expand assignment of first value to ebx
            ASTElement divisionLeftPart = node.GetChild(contextType.CT_EXPRESSION_DIVISION_LEFT, 0);

            if (divisionLeftPart.GetType().Equals(typeof(CASTIDENTIFIER)) || divisionLeftPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov ebx," + divisionLeftPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(divisionLeftPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ebx,eax\n");
            }

            //if second value not identifier or value, expand assignment of second value to ecx
            ASTElement divisionRightPart = node.GetChild(contextType.CT_EXPRESSION_DIVISION_RIGHT, 0);
            if (divisionRightPart.GetType().Equals(typeof(CASTIDENTIFIER)) || divisionRightPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov ecx," + divisionRightPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(divisionRightPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA,
                    M_FunctionParent = param.M_FunctionParent,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalParent = param.M_ConditionalParent,
                    M_ConditionalCase = param.M_ConditionalCase
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ecx,eax\n");
            }


            rep.AddCode("\n" + "mov eax,ebx" +
                        "\n" + "div eax,ecx" +
                        "\n" + "pop ebx" +
                        "\n" + "pop ecx");
            return rep;
        }

        public override CEmmitableCodeContainer VisitParenthesis(CASTExpressionInParenthesis node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            //rep.AddCode("(");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_PARENTHESIS, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA,
                M_FunctionParent = param.M_FunctionParent,
                M_LoopParent = param.M_LoopParent,
                M_ConditionalParent = param.M_ConditionalParent,
                M_ConditionalCase = param.M_ConditionalCase
            }).AssemblyCodeContainer());
            //rep.AddCode(")");
            return rep;
        }

        //public override CEmmitableCodeContainer VisitPLUS(CASTExpressionPlus node,
        //    TranslationParameters param = default(TranslationParameters))
        //{
        //    CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
        //    param.M_Parent?.AddCode(rep, param.M_ParentContextType);
        //    rep.AddCode("+");
        //    rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_PLUS, 0), new TranslationParameters()
        //    {
        //        M_ContainerFunction = param.M_ContainerFunction,
        //        M_Parent = null,
        //        M_ParentContextType = CodeContextType.CC_NA
        //    }).AssemblyCodeContainer());
        //    return rep;
        //}

        //public override CEmmitableCodeContainer VisitMINUS(CASTExpressionMinus node,
        //    TranslationParameters param = default(TranslationParameters))
        //{
        //    CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
        //    param.M_Parent?.AddCode(rep, param.M_ParentContextType);
        //    rep.AddCode("-");
        //    rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_MINUS, 0), new TranslationParameters()
        //    {
        //        M_ContainerFunction = param.M_ContainerFunction,
        //        M_Parent = null,
        //        M_ParentContextType = CodeContextType.CC_NA
        //    }).AssemblyCodeContainer());
        //    return rep;
        //}

        public override CEmmitableCodeContainer VisitNOT(CASTExpressionNot node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode("!");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_NOT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitAND(CASTExpressionAnd node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_AND_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("&&");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_AND_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitOR(CASTExpressionOr node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_OR_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("||");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_OR_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitGT(CASTExpressionGt node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            //param.M_Parent?.AddCode(rep, param.M_ParentContextType);

            rep.AddCode("cmp " + node.GetChildrenList()[0] + "," + node.GetChildrenList()[2]);

            String labelString = "";
            if (param.M_ConditionalCase.Equals("if"))
            {
                labelString = "IFSTATEMENT" + param.M_ConditionalParent.MSerial.ToString();
            } else if (param.M_ConditionalCase.Equals("while"))
            {
                labelString = "WHILESTATEMENT" + param.M_LoopParent.MSerial.ToString();
            }
            rep.AddCode("jb " + labelString);


            //rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_GT_LEFT, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_Parent = null,
            //    M_ParentContextType = CodeContextType.CC_NA
            //}).AssemblyCodeContainer());
            //rep.AddCode(">");
            //rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_GT_RIGHT, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_Parent = null,
            //    M_ParentContextType = CodeContextType.CC_NA
            //}).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitGTE(CASTExpressionGte node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            //param.M_Parent?.AddCode(rep, param.M_ParentContextType);

            rep.AddCode("cmp " + node.GetChildrenList()[0] + "," + node.GetChildrenList()[2]);

            String labelString = "";
            if (param.M_ConditionalCase.Equals("if"))
            {
                labelString = "IFSTATEMENT" + param.M_ConditionalParent.MSerial.ToString();
            } else if (param.M_ConditionalCase.Equals("while"))
            {
                labelString = "WHILESTATEMENT" + param.M_LoopParent.MSerial.ToString();
            }
            rep.AddCode("jbe " + labelString);
            return rep;
        }

        public override CEmmitableCodeContainer VisitLT(CASTExpressionLt node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            //param.M_Parent?.AddCode(rep, param.M_ParentContextType);

            rep.AddCode("cmp " + node.GetChildrenList()[0] + "," + node.GetChildrenList()[2]);

            String labelString = "";
            if (param.M_ConditionalCase.Equals("if"))
            {
                labelString = "IFSTATEMENT" + param.M_ConditionalParent.MSerial.ToString();
            } else if (param.M_ConditionalCase.Equals("while"))
            {
                labelString = "WHILESTATEMENT" + param.M_LoopParent.MSerial.ToString();
            }
            rep.AddCode("jl " + labelString);
            return rep;
        }

        public override CEmmitableCodeContainer VisitLTE(CASTExpressionLte node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            //param.M_Parent?.AddCode(rep, param.M_ParentContextType);

            rep.AddCode("cmp " + node.GetChildrenList()[0] + "," + node.GetChildrenList()[2]);

            String labelString = "";
            if (param.M_ConditionalCase.Equals("if"))
            {
                labelString = "IFSTATEMENT" + param.M_ConditionalParent.MSerial.ToString();
            } else if (param.M_ConditionalCase.Equals("while"))
            {
                labelString = "WHILESTATEMENT" + param.M_LoopParent.MSerial.ToString();
            }
            rep.AddCode("jle " + labelString);
            return rep;
        }

        public override CEmmitableCodeContainer VisitEQUAL(CASTExpressionEqual node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            //param.M_Parent?.AddCode(rep, param.M_ParentContextType);

            rep.AddCode("cmp " + node.GetChildrenList()[0] + "," + node.GetChildrenList()[2]);

            String labelString = "";
            if (param.M_ConditionalCase.Equals("if"))
            {
                labelString = "IFSTATEMENT" + param.M_ConditionalParent.MSerial.ToString();
            } else if (param.M_ConditionalCase.Equals("while"))
            {
                labelString = "WHILESTATEMENT" + param.M_LoopParent.MSerial.ToString();
            }
            rep.AddCode("je " + labelString);
            return rep;
        }

        public override CEmmitableCodeContainer VisitNEQUAL(CASTExpressionNequal node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            //param.M_Parent?.AddCode(rep, param.M_ParentContextType);

            rep.AddCode("cmp " + node.GetChildrenList()[0] + "," + node.GetChildrenList()[2]);

            String labelString = "";
            if (param.M_ConditionalCase.Equals("if"))
            {
                labelString = "IFSTATEMENT" + param.M_ConditionalParent.MSerial.ToString();
            } else if (param.M_ConditionalCase.Equals("while"))
            {
                labelString = "WHILESTATEMENT" + param.M_LoopParent.MSerial.ToString();
            }
            rep.AddCode("jne " + labelString);
            return rep;
        }

        public override CEmmitableCodeContainer VisitWHILESTATEMENT(CASTWhileStatement node,
            TranslationParameters param = default(TranslationParameters))
        {
            CWhileStatement rep = new CWhileStatement(param.M_Parent);
            rep.AddCode("WHILESTATEMENT" + node.MSerial.ToString() + ": ", CodeContextType.CC_WHILESTATEMENT_BODY);

            //visit condition
            Visit(node.GetChild(contextType.CT_WHILESTATEMENT_CONDITION, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_ParentContextType = CodeContextType.CC_WHILESTATEMENT_CONDITION,
                M_Parent = rep,
                M_FunctionParent = param.M_FunctionParent,
                M_LoopParent = node,
                M_ConditionalCase = "while"
            });

            rep.AddCode("jmp OUTWHILESTATEMENT" + node.MSerial.ToString(), CodeContextType.CC_WHILESTATEMENT_BODY);
            rep.AddCode("INSIDEWHILESTATEMENT" + node.MSerial.ToString() + ": ", CodeContextType.CC_WHILESTATEMENT_BODY);

            // visit body code
            Visit(node.GetChild(contextType.CT_WHILESTATEMENT_BODY, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_ParentContextType = CodeContextType.CC_WHILESTATEMENT_BODY,
                M_FunctionParent = param.M_FunctionParent,
                M_LoopParent = node,
                M_ConditionalCase = param.M_ConditionalCase,
                M_Parent = rep
            });

            rep.AddCode("jmp WHILESTATEMENT" + node.MSerial.ToString(), CodeContextType.CC_WHILESTATEMENT_BODY);
            rep.AddCode("OUTSIDEWHILESTATEMENT" + node.MSerial.ToString() + ": ", CodeContextType.CC_WHILESTATEMENT_BODY);

            //param.M_Parent?.AddCode(rep1, param.M_ParentContextType);
            //Visit(node.GetChild(contextType.CT_WHILESTATEMENT_CONDITION, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_ParentContextType = CodeContextType.CC_WHILESTATEMENT_CONDITION,
            //    M_Parent = rep1
            //});
            //Visit(node.GetChild(contextType.CT_WHILESTATEMENT_BODY, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_ParentContextType = CodeContextType.CC_WHILESTATEMENT_BODY,
            //    M_Parent = rep1
            //});
            return rep;
        }

        public override CEmmitableCodeContainer VisitIFSTATEMENT(CASTIfStatement node,
            TranslationParameters param = default(TranslationParameters))
        {
            CIfStatement rep = new CIfStatement(param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            Type conditionType = node.GetChild(contextType.CT_IFSTATEMENT_CONDITION, 0).GetType();

            if (conditionType.Equals(typeof(CASTExpressionAnd))){
                ASTElement leftConditional = node.GetChild(contextType.CT_IFSTATEMENT_CONDITION, 0).GetChildrenList()[0].First();
                ASTElement rightConditional = node.GetChild(contextType.CT_IFSTATEMENT_CONDITION, 0).GetChildrenList()[1].First();

            } else if(conditionType.Equals(typeof(CASTExpressionOr)))
            {
                ASTElement leftConditional = node.GetChild(contextType.CT_IFSTATEMENT_CONDITION, 0).GetChildrenList()[0].First();
                ASTElement rightConditional = node.GetChild(contextType.CT_IFSTATEMENT_CONDITION, 0).GetChildrenList()[1].First();
            }
            else if (conditionType.Equals(typeof(CASTExpressionNot)))
            {

            } else{
                Visit(node.GetChild(contextType.CT_IFSTATEMENT_CONDITION, 0), new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_ParentContextType = CodeContextType.CC_IFSTATEMENT_CONDITION,
                    M_FunctionParent = param.M_FunctionParent,
                    M_ConditionalParent = node,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalCase = "if",
                    M_Parent = rep
                });

                rep.AddCode("jmp ELSESTATEMENT" + node.MSerial + "\n", CodeContextType.CC_IFSTATEMENT_IFBODY);

                rep.AddCode("INSIDEIFSTATEMENT" + node.MSerial + ":\n", CodeContextType.CC_IFSTATEMENT_CONDITION);
                Visit(node.GetChild(contextType.CT_IFSTATEMENT_IFCLAUSE, 0), new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_ParentContextType = CodeContextType.CC_IFSTATEMENT_IFBODY,
                    M_FunctionParent = param.M_FunctionParent,
                    M_ConditionalParent = node,
                    M_LoopParent = param.M_LoopParent,
                    M_ConditionalCase = "if",
                    M_Parent = rep
                });
                rep.AddCode("jmp OUTSIDEIFSTATEMENT" + node.MSerial + "\n", CodeContextType.CC_IFSTATEMENT_IFBODY);


                rep.AddCode("ELSESTATEMENT" + node.MSerial + ":\n", CodeContextType.CC_IFSTATEMENT_CONDITION);


                if (node.GetContextChildren(contextType.CT_IFSTATEMENT_ELSECLAUSE).Length != 0)
                {
                    Visit(node.GetChild(contextType.CT_IFSTATEMENT_ELSECLAUSE, 0), new TranslationParameters()
                    {
                        M_ContainerFunction = param.M_ContainerFunction,
                        M_ParentContextType = CodeContextType.CC_IFSTATEMENT_ELSEBODY,
                        M_FunctionParent = param.M_FunctionParent,
                        M_ConditionalParent = node,
                        M_LoopParent = param.M_LoopParent,
                        M_ConditionalCase = "if",
                        M_Parent = rep
                    });
                }

                rep.AddCode("OUTSIDEIFSTATEMENT" + node.MSerial + "\n", CodeContextType.CC_IFSTATEMENT_IFBODY);
            }

            

            return rep;
        }

        public override CEmmitableCodeContainer VisitCOMPOUNDSTATEMENT(CASTCompoundStatement node,
            TranslationParameters param = default(TranslationParameters))
        {

            CCompoundStatement cmpst = new CCompoundStatement(param.M_Parent);
            param.M_Parent?.AddCode(cmpst, param.M_ParentContextType);
            VisitContext(node, contextType.CT_STATEMENT_COMPOUNDSTATEMENT, new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = cmpst,
                M_FunctionParent = param.M_FunctionParent,
                M_LoopParent = param.M_LoopParent,
                M_ConditionalParent = param.M_ConditionalParent,    
                M_ConditionalCase = param.M_ConditionalCase, 
                M_ParentContextType = CodeContextType.CC_COMPOUNDSTATEMENT_BODY
            });

            return cmpst;
        }

        public override CEmmitableCodeContainer VisitSTATEMENTEXPRESSION(CASTEpxressionStatement node,
            TranslationParameters param = default(TranslationParameters))
        {
            CExpressionStatement rep = new CExpressionStatement(param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            Visit(node.GetChild(contextType.CT_STATEMENT_EXPRESSION, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = rep,
                M_FunctionParent = param.M_FunctionParent,
                M_LoopParent = param.M_LoopParent,
                M_ConditionalParent = param.M_ConditionalParent,
                M_ConditionalCase = param.M_ConditionalCase,
                M_ParentContextType = CodeContextType.CC_EXPRESSIONSTATEMENT_BODY
            });
            return rep;
        }

        public override CEmmitableCodeContainer VisitSTATEMENTRETURN(CASTReturnStatement node,
            TranslationParameters param = default(TranslationParameters))
        {
            CReturnStatement rep = new CReturnStatement(param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            ASTElement returnValue = node.GetChild(contextType.CT_STATEMENT_RETURN, 0);
            if (returnValue != null) {

            rep.AddCode("mov eax," + returnValue.M_Text + "\n", 
                CodeContextType.CC_RETURNSTATEMENT_BODY);
            }
            //rep.AddCode("ret\n", CodeContextType.CC_RETURNSTATEMENT_BODY); that here is wrong, dont do it

            
            rep.AddCode("jmp " + param.M_FunctionParent.GetFunctionName() + "END", CodeContextType.CC_RETURNSTATEMENT_BODY);

            //Visit(node.GetChild(contextType.CT_STATEMENT_RETURN, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_Parent = rep,
            //    M_ParentContextType = CodeContextType.CC_RETURNSTATEMENT_BODY
            //});
            return rep;
        }

        public override CEmmitableCodeContainer VisitSTATEMENTBREAK(CASTBreakStatement node,
            TranslationParameters param = default(TranslationParameters))
        {

            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            rep.AddCode("jmp " + param.M_LoopParent.MSerial.ToString() + "END", CodeContextType.CC_RETURNSTATEMENT_BODY);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            return rep;
        }

        public override CEmmitableCodeContainer VisitIDENTIFIER(CASTIDENTIFIER node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_ContainerFunction.DeclareVariable(node.M_Text, true);
            rep.AddCode(node.M_Text);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            return rep;
        }

        public override CEmmitableCodeContainer VisitNUMBER(CASTNUMBER node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            rep.AddCode(node.M_Text);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            return rep;
        }




        public override CEmmitableCodeContainer VisitTYPESPECIFIERINT(CASTTypespecifierInt node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            return rep;
        }

        public override CEmmitableCodeContainer VisitTYPESPECIFIERDOUBLE(CASTTypespecifierDouble node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            return rep;
        }

        public override CEmmitableCodeContainer VisitTYPESPECIFIERFLOAT(CASTTypespecifierFloat node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            return rep;
        }

        public override CEmmitableCodeContainer VisitTYPESPECIFIERCHAR(CASTTypespecifierChar node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            return rep;
        }

        public override CEmmitableCodeContainer VisitTYPESPECIFIERVOID(CASTTypespecifierVoid node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            return rep;
        }

        public override CEmmitableCodeContainer VisitDATADECLARATION(CASTDatadeclaration node, TranslationParameters param = default(TranslationParameters))
        {
            //CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            //CDatadeclaration data = new CDatadeclaration(param.M_Parent);
            //data.AddCode("melemeths foygaros", CodeContextType.);
            //return rep;

            CCFunctionDefinition rep = new CCFunctionDefinition(param.M_Parent);

            Type type = node.GetContextChildren(contextType.CT_DATADECLARATION_TYPESPECIFIER)[0].GetChildrenList()[0].GetType();
            ASTElement identifier = node.GetContextChildren(contextType.CT_DATADECLARATION_IDENTIFIER)[0];
            ASTElement[] datavalue = node.GetContextChildren(contextType.CT_DATADECLARATION_DATAVALUE);

            String typeString = "", identifierString = "", datavalueString = "";

            // we need to find the type first
            if (type.Equals(typeof(CASTCHAR_TYPE)) || type.Equals(typeof(CASTVOID_TYPE)))
            {
                typeString = "SBYTE";
            }
            else if (type.Equals(typeof(CASTINT_TYPE)))
            {
                typeString = "SWORD";
            }
            else if (type.Equals(typeof(CASTDOUBLE_TYPE)) || type.Equals(typeof(CASTFLOAT_TYPE)))
            {
                typeString = "SDWORD";
            }


            //identifierString = identifier.m_name_text;

            if (datavalue.Length != 0 && datavalue != null)
            {
                typeString = "?";
            } else
            {
                //typeString = datavalue[0].m_name_text;
            }

            rep.AddCode(datavalueString + " " + identifierString + " " + datavalue + "\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            return rep;
        }

        public override CEmmitableCodeContainer VisitDATAVALUE(CASTDatavalue node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            return rep;
        }
        public override CEmmitableCodeContainer VisitFUNCTIONBODY(CASTFunctionBody node, TranslationParameters param)
        {
            CCFunctionbody rep1 = new CCFunctionbody(param.M_Parent);
            param.M_Parent?.AddCode(rep1, param.M_ParentContextType);

            Visit(node.GetChild(contextType.CT_FUNCTIONBODY_STATEMENT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_ParentContextType = CodeContextType.CC_FUNCTIONDEFINITION_BODY,
                M_Parent = rep1
            });


            var test = 0;

            return rep1;
        }
        //public override int VisitFormalargs(testparser.FormalargsContext context)
        //{


        //}
    }
}
