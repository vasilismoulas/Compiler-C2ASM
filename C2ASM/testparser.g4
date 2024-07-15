﻿parser grammar testparser;

options { tokenVocab = testlexer; }

/*Parser Rules*/
compileUnit : (functionDefinition|globalstatement)+
			;

globalstatement : functionDeclaration QM                                     
                ;

functionDeclaration : funprefix formalargs? RP		
                    ;

functionDefinition :  funprefix formalargs? RP '{' functionbody '}'	 #custom_FunctionDefinition		
				   ;

funprefix : typespecifier IDENTIFIER LP
          ;

functionbody : (statement)+ 
			 ;


statement : expr QM		        #statement_ExpressionStatement
		  | ifstatement			#statement_IfStatement
		  | whilestatement		#statement_WhileStatement
		  | compoundStatement	#statement_CompoundStatement
		  | datadeclaration QM     #statement_DataDeclarationStatement
		  | RETURN expr QM      #statement_ReturnStatement
		  | BREAK QM			#statement_BreakStatement
		  ;

ifstatement : IF LP expr RP statement (ELSE statement)?
			;

whilestatement : WHILE LP expr RP statement
			   ;

compoundStatement : LB RB
				  | LB statementList RB
				  ;

statementList : (statement)+ 
			  ;

datadeclaration: typespecifier IDENTIFIER ('=' datavalue)?
               ;

datavalue : NUMBER		#datavalue_Number
		  | CHAR		#datavalue_Char
		  ;

typespecifier : INT_TYPE		#typespecifier_IntType
			  | DOUBLE_TYPE     #typespecifier_DoubleType
			  | FLOAT_TYPE      #typespecifier_FloatType
			  | CHAR_TYPE       #typespecifier_CharType
			  | VOID_TYPE       #typespecifier_VoidType
			  ;


expr       : NUMBER								#expr_NUMBER	
		   | IDENTIFIER							#expr_IDENTIFIER
		   | CHAR                               #expr_CHAR
		   | IDENTIFIER LP args RP				#expr_FCALL
		   | expr op=(DIV|MULT) expr 			#expr_MULDIV   
		   | expr op=(PLUS|MINUS) expr		    #expr_PLUSMINUS
		   | PLUS expr							#expr_PLUS
		   | MINUS expr							#expr_MINUS
		   | LP expr RP							#expr_PAREN
		   | IDENTIFIER ASSIGN expr     		#expr_ASSIGN
		   | NOT expr							#expr_NOT
	       | expr AND expr						#expr_AND
		   | expr OR expr						#expr_OR
		   | expr GT expr						#expr_GT
		   | expr GTE expr						#expr_GTE
		   | expr LT expr						#expr_LT
		   | expr LTE expr						#expr_LTE
		   | expr EQUAL expr					#expr_EQUAL
		   | expr NEQUAL expr					#expr_NEQUAL
		   ;

//arguments for function calls
args : (expr (COMMA)?)+  
	 ;

//formal function arguments/function parameters
formalargs : (datadeclaration (COMMA)?)+
	  ;

