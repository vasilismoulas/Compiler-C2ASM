lexer grammar testlexer;

/*Lexer Rules*/

// Reserved words
FUNCTION :'function';
MAIN: 'main';
RETURN :'return'; 
IF:'if';
ELSE:'else';
WHILE:'while';
BREAK: 'break';

// Operators
PLUS:'+'; 
MINUS:'-';
DIV:'/'; 
MULT:'*';
OR:'||';
AND:'&&';
NOT:'!';
EQUAL:'==';
NEQUAL:'!='; 
GT:'>';
LT:'<';
GTE:'>=';
LTE:'<=';
QM:';';
LP:'(';
RP:')';
LB:'{';
RB:'}'; 
COMMA:',';
ASSIGN:'=';

// Data Types
INT_TYPE: 'int';	 
DOUBLE_TYPE: 'double';
FLOAT_TYPE: 'float';
CHAR_TYPE: 'char';
VOID_TYPE: 'void';   

// Identifiers - Numbers - Char
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;
NUMBER: [1-9][0-9]*|'0';
CHAR: '\'' (~('\'' | '\\') | '\\' .) '\'';

// Whitespace
WS: [ \r\n\t]-> skip;