//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_ASM;
using C2ASM;

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


        public TranslationParameters()
        {

        }

        public TranslationParameters(CEmmitableCodeContainer m_parent, CCFunctionDefinition m_containerFunction, CodeContextType m_parentContextType)
        {
            this.m_parent = m_parent;
            this.m_containerFunction = m_containerFunction;
            this.m_parentContextType = m_parentContextType;
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
    }

    class C2ASMTranslation : ASTBaseVisitor<CEmmitableCodeContainer, TranslationParameters>
    {
        private CCFile m_translatedFile;

        public CCFile M_TranslatedFile => m_translatedFile;


        public override CEmmitableCodeContainer VisitCOMPILEUNIT(CASTCompileUnit node, TranslationParameters param)
        {
            //CCFunctionDefinition mainf;
            //1. Create Output File
            m_translatedFile = new CCFile(true);
            //mainf = m_translatedFile.MainDefinition;

            m_translatedFile.AddCode(".model small \n", CodeContextType.CC_FILE_PREPROCESSOR);
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

            CCFunctionDefinition fundef = new CCFunctionDefinition(param.M_Parent);

            //2. Add Function Definition to the File in the appropriate context
            param.M_Parent.AddCode(fundef, param.M_ParentContextType);

            //3. Assemble the function header
            CASTFunprefix funprefixthing = node.GetChild(contextType.CT_FUNCTIONDEFINITION_FUNPREFIX, 0) as CASTFunprefix;
            CASTIDENTIFIER id = funprefixthing.GetChild(contextType.CT_FUNPREFIX_IDENTIFIER, 0) as CASTIDENTIFIER;
            //CASTFormalArgs functionArguments = node.GetChild(contextType.CT_FUNCTIONDEFINITION_FARGUMENTS, 0) as CASTFormalArgs;
            //CASTFunctionBody functionBody = node.GetChild(contextType.CT_FUNCTIONDEFINITION_BODY, 0) as CASTFunctionBody;
            fundef.EnterScope();

            fundef.AddCode(id.M_Text + " PROC" + "\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            fundef.AddCode( "\tpush ebp"    + "\n"
                            + "mov ebp,esp" + "\n"
                            + "push eax"    + "\n"
                            + "push ebx"    + "\n"
                            + "push ecx"    + "\n"
                            + "push edx"    + "\n"
                            + "push esi"    + "\n"
                            + "push edi"    + "\n" + "\n"
                            , CodeContextType.CC_FUNCTIONDEFINITION_HEADER);

            // add argument variables
            fundef.AddCode("\n",CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            fundef.AddCode("\t;Arguments\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);

            // add inside code visitor
            fundef.AddCode("\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            fundef.AddCode("\t;Function body\n", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);


            ASTElement child = node.GetChild(contextType.CT_FUNCTIONDEFINITION_BODY, 0);
            CEmmitableCodeContainer repo = Visit(child, new TranslationParameters()
            {
                M_ContainerFunction = fundef,
                M_ParentContextType = CodeContextType.CC_FUNCTIONDEFINITION_BODY,
                M_Parent = fundef
            });

            //CEmmitableCodeContainer repo = VisitContext(node, contextType.CT_FUNCTIONDEFINITION_BODY,
            //new TranslationParameters()
            //{
            //    M_Parent = m_translatedFile,
            //    M_ParentContextType = CodeContextType.CC_FILE_FUNDEF,
            //    M_ContainerFunction = fundef
            //});



            fundef.AddCode(repo, CodeContextType.CC_FUNCTIONDEFINITION_BODY);



            //param.M_Parent?.AddCode(fundef, param.M_ParentContextType);


            //fundef.AddCode(VisitContext(node, contextType.CT_FUNCTIONDEFINITION_BODY,
            //    new TranslationParameters()
            //    {
            //        M_Parent = param.M_Parent,
            //        M_ParentContextType = CodeContextType.CC_FILE_FUNDEF
            //    }), CodeContextType.CC_FUNCTIONDEFINITION_BODY);

            //fundef.AddCode(Visit(node.GetChild(contextType.CT_FUNCTIONDEFINITION_BODY, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = fundef,
            //    M_ParentContextType = CodeContextType.CC_FUNCTIONDEFINITION_BODY,
            //    M_Parent = fundef
            //}), CodeContextType.CC_FUNCTIONDEFINITION_BODY);

            //rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_ASSIGN_EXPRESSION, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_Parent = null,
            //    M_ParentContextType = CodeContextType.CC_NA
            //}).AssemblyCodeContainer());



            //CEmmitableCodeContainer bodyContainer = Visit(body, new TranslationParameters()
            //{
            //    M_ContainerFunction = fundef,
            //    M_Parent = fundef,
            //    M_ParentContextType = param.M_ParentContextType
            //});

            //VisitContext(node, contextType.CT_FUNCTION,
            //    new TranslationParameters()
            //    {
            //        M_Parent = m_translatedFile,
            //        M_ParentContextType = CodeContextType.CC_FILE_FUNDEF
            //    });




            fundef.AddCode("\n" + id.M_Text+"END:"     + "\n"
                            + "pop edi"         + "\n"
                            + "pop esi"         + "\n"
                            + "pop eax"         + "\n"
                            + "pop edx"         + "\n"
                            + "pop ecx"         + "\n"
                            + "pop ebx"         + "\n"
                            + "mov esp,ebp"     + "\n"
                            + "pop ebp"         + "\n"
                            + "ret"             + "\n", CodeContextType.CC_FUNCTIONDEFINITION_BODY);
            fundef.LeaveScope();
            fundef.AddCode(id.M_Text + "ENDP" + "\n", CodeContextType.CC_FUNCTIONDEFINITION_BODY);

            //fundef.AddCode("float " + id.M_Text + "(", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            //string last = "";
            //string[] lastString = node.GetFunctionArgs();
            //if (lastString.Length != 0)
            //{
            //    last = lastString.Last();
            //}
            //foreach (string s in node.GetFunctionArgs())
            //{
            //    fundef.AddCode("float " + s, CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            //    if (!s.Equals(last))
            //    {
            //        fundef.AddCode(", ", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);
            //    }
            //    fundef.AddVariableToLocalSymbolTable(s);
            //}
            //fundef.AddCode(")", CodeContextType.CC_FUNCTIONDEFINITION_HEADER);



            return fundef;
        }

        

        public override CEmmitableCodeContainer VisitASSIGN(CASTExpressionAssign node, TranslationParameters param = default(TranslationParameters))
        {
            CCFunctionDefinition fun = param.M_ContainerFunction as CCFunctionDefinition;

            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);

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
                    M_ParentContextType = CodeContextType.CC_NA
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov " + assignLeftPart.m_name_text + ",eax\n");
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

            CASTIDENTIFIER id = node.GetChild(contextType.CT_EXPRESSION_FCALLNAME, 0) as CASTIDENTIFIER;
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
                rep.AddCode("mov eax," + additionLeftPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(additionLeftPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ebx,eax\n");
            }

            //if second value not identifier or value, expand assignment of second value to ecx
            ASTElement additionRightPart = node.GetChild(contextType.CT_EXPRESSION_ADDITION_LEFT, 0);
            if (additionRightPart.GetType().Equals(typeof(CASTIDENTIFIER)) || additionRightPart.GetType().Equals(typeof(CASTNUMBER)))
            {
                rep.AddCode("mov eax," + additionRightPart.m_name_text + "\n");
            } else
            {
                //1. Visit
                rep.AddCode(Visit(additionRightPart, new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_Parent = null,
                    M_ParentContextType = CodeContextType.CC_NA
                }).AssemblyCodeContainer());

                //2. Then add eax
                rep.AddCode("mov ebx,eax\n");
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

        public override CEmmitableCodeContainer VisitMultiplication(CASTExpressionMultiplication node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_MULTIPLICATION_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("*");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_MULTIPLICATION_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitDivision(CASTExpressionDivision node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_DIVISION_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("/");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_DIVISION_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitSubtraction(CASTExpressionSubtraction node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_SUBTRACTION_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("-");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_SUBTRACTION_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitParenthesis(CASTExpressionInParenthesis node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode("(");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_PARENTHESIS, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode(")");
            return rep;
        }

        public override CEmmitableCodeContainer VisitPLUS(CASTExpressionPlus node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode("+");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_PLUS, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitMINUS(CASTExpressionMinus node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode("-");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_MINUS, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

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
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_GT_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode(">");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_GT_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitGTE(CASTExpressionGte node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_GTE_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode(">=");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_GTE_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitLT(CASTExpressionLt node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_LT_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("<");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_LT_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitLTE(CASTExpressionLte node, TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_LTE_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("<=");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_LTE_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitEQUAL(CASTExpressionEqual node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_EQUAL_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("==");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_EQUAL_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitNEQUAL(CASTExpressionNequal node,
            TranslationParameters param = default(TranslationParameters))
        {
            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_NEQUAL_LEFT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            rep.AddCode("!=");
            rep.AddCode(Visit(node.GetChild(contextType.CT_EXPRESSION_NEQUAL_RIGHT, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = null,
                M_ParentContextType = CodeContextType.CC_NA
            }).AssemblyCodeContainer());
            return rep;
        }

        public override CEmmitableCodeContainer VisitWHILESTATEMENT(CASTWhileStatement node,
            TranslationParameters param = default(TranslationParameters))
        {
            CWhileStatement rep1 = new CWhileStatement(param.M_Parent);
            param.M_Parent?.AddCode(rep1, param.M_ParentContextType);
            Visit(node.GetChild(contextType.CT_WHILESTATEMENT_CONDITION, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_ParentContextType = CodeContextType.CC_WHILESTATEMENT_CONDITION,
                M_Parent = rep1
            });
            Visit(node.GetChild(contextType.CT_WHILESTATEMENT_BODY, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_ParentContextType = CodeContextType.CC_WHILESTATEMENT_BODY,
                M_Parent = rep1
            });
            return rep1;
        }

        public override CEmmitableCodeContainer VisitIFSTATEMENT(CASTIfStatement node,
            TranslationParameters param = default(TranslationParameters))
        {
            CIfStatement rep1 = new CIfStatement(param.M_Parent);
            param.M_Parent?.AddCode(rep1, param.M_ParentContextType);
            Visit(node.GetChild(contextType.CT_IFSTATEMENT_CONDITION, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_ParentContextType = CodeContextType.CC_IFSTATEMENT_CONDITION,
                M_Parent = rep1
            });
            Visit(node.GetChild(contextType.CT_IFSTATEMENT_IFCLAUSE, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_ParentContextType = CodeContextType.CC_IFSTATEMENT_IFBODY,
                M_Parent = rep1
            });
            if (node.GetContextChildren(contextType.CT_IFSTATEMENT_ELSECLAUSE).Length != 0)
            {
                Visit(node.GetChild(contextType.CT_IFSTATEMENT_ELSECLAUSE, 0), new TranslationParameters()
                {
                    M_ContainerFunction = param.M_ContainerFunction,
                    M_ParentContextType = CodeContextType.CC_IFSTATEMENT_ELSEBODY,
                    M_Parent = rep1
                });
            }
            return rep1;
        }

        public override CEmmitableCodeContainer VisitCOMPOUNDSTATEMENT(CASTCompoundStatement node,
            TranslationParameters param = default(TranslationParameters))
        {

            CCompoundStatement cmpst = new CCompoundStatement(param.M_Parent);
            //param.M_Parent?.AddCode(cmpst, param.M_ParentContextType);
            //VisitContext(node, contextType.CT_STATEMENT_COMPOUNDSTATEMENT, new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_Parent = cmpst,
            //    M_ParentContextType = CodeContextType.CC_COMPOUNDSTATEMENT_BODY
            //});

            return cmpst;
        }

        public override CEmmitableCodeContainer VisitSTATEMENTEXPRESSION(CASTEpxressionStatement node,
            TranslationParameters param = default(TranslationParameters))
        {
            CExpressionStatement rep = new CExpressionStatement(param.M_Parent);
            //param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            //Visit(node.GetChild(contextType.CT_STATEMENT_EXPRESSION, 0), new TranslationParameters()
            //{
            //    M_ContainerFunction = param.M_ContainerFunction,
            //    M_Parent = rep,
            //    M_ParentContextType = CodeContextType.CC_EXPRESSIONSTATEMENT_BODY
            //});
            return rep;
        }

        public override CEmmitableCodeContainer VisitSTATEMENTRETURN(CASTReturnStatement node,
            TranslationParameters param = default(TranslationParameters))
        {
            CReturnStatement rep = new CReturnStatement(param.M_Parent);
            param.M_Parent?.AddCode(rep, param.M_ParentContextType);
            Visit(node.GetChild(contextType.CT_STATEMENT_RETURN, 0), new TranslationParameters()
            {
                M_ContainerFunction = param.M_ContainerFunction,
                M_Parent = rep,
                M_ParentContextType = CodeContextType.CC_RETURNSTATEMENT_BODY
            });
            return rep;
        }

        public override CEmmitableCodeContainer VisitSTATEMENTBREAK(CASTBreakStatement node,
            TranslationParameters param = default(TranslationParameters))
        {

            CodeContainer rep = new CodeContainer(CodeBlockType.CB_CODEREPOSITORY, param.M_Parent);
            rep.AddCode("break;");
            rep.AddNewLine();
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
    }
}
