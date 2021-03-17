lexer grammar AutoFilterLexer;

@lexer::header {#pragma warning disable 3021}

PAREN_OPEN: '(';
PAREN_CLOSE: ')';
COMMA: ',';
COLON: ':';
SLASH: '/';
DOT: '.';

OR: '|';
//AND: ',';

DESC: '-';

NONE: 'none';

ASSIGN: '=';
EQUALS: '==';
NOTEQUALS: '!=';
GREATERTHAN: '>';
GREATERTHANOREQUAL: '>=';
LESSTHAN: '<';
LESSTHANOREQUAL: '<=';
NOT: '!';
STARTSWITH: '_=';
NOTSTARTSWITH: '!_=';
ENDSWITH: '^=';
NOTENDSWITH: '!^=';
CONTAINS: '@=';
NOTCONTAINS: '!@=';
CASEIGNORECONTAINS: '@=*';
CASEIGNORENOTCONTAINS: '!@=*';
CASEIGNORESTARTSWITH: '_=*';
CASEIGNOREENDSWITH: '^=*';
CASEIGNOREEQUALS: '==*';
CASEIGNORENOTEQUALS: '!=*';
CASEIGNORENOTSTARTSWITH: '!_=*';
CASEIGNORENOTENDSWITH: '!^=*';

TOLOWER: 'tolower';

TOUPPER: 'toupper';

YEAR: 'year';

YEARS: 'years';

MONTH: 'month';

DAY: 'day';

DAYS: 'days';

HOUR: 'hour';

HOURS: 'hours';

MINUTE: 'minute';

MINUTES: 'minutes';

SECOND: 'second';

SECONDS: 'seconds';

ANY: 'any';

ALL: 'all';

COUNT: 'count';

MIN: 'min';

MAX: 'max';

SUM: 'sum';

AVERAGE: 'average';
INT: ('-')? '0'..'9'+;

LONG: ('-')? ('0'..'9')+ 'L';

DOUBLE: ('-')? ('0'..'9')+ '.' ('0'..'9')+ 'd'?;

SINGLE: ('-')? ('0'..'9')+ '.' ('0'..'9')+ 'f';

DECIMAL: ('-')? ('0'..'9')+ '.' ('0'..'9')+ 'm';

BOOL: ('true' | 'false');

NULL: 'null';

DATETIME: 'datetime\'' '0'..'9'+ '-' '0'..'9'+ '-' + '0'..'9'+ 'T' '0'..'9'+ ':' '0'..'9'+ (':' '0'..'9'+ ('.' '0'..'9'+)*)* ('Z')? '\'';

GUID: 'guid\'' HEX_PAIR HEX_PAIR HEX_PAIR HEX_PAIR '-' HEX_PAIR HEX_PAIR '-' HEX_PAIR HEX_PAIR '-' HEX_PAIR HEX_PAIR '-' HEX_PAIR HEX_PAIR HEX_PAIR HEX_PAIR HEX_PAIR HEX_PAIR '\'';

BYTE: '0x' HEX_PAIR;

SPACE: (' '|'\t')+;

NEWLINE: ('\r'|'\n')+;

DYNAMICIDENTIFIER: '[' ('a'..'z'|'A'..'Z'|'0'..'9'|'_')+ ']';

fragment HEX_PAIR: HEX_DIGIT HEX_DIGIT;

IDENTIFIER: [a-z_A-Z0-9]* [a-zA-Z0-9];

STRING: '\'' (ESC_SEQ| ~('\\'|'\''))* '\'';

fragment HEX_DIGIT: ('0'..'9'|'a'..'f'|'A'..'F') ;

fragment ESC_SEQ : '\'\''
		| '\\' ('b'|'t'|'n'|'f'|'r'|'"'|'\''|'\\')
		| UNICODE_ESC
		| OCTAL_ESC;

fragment OCTAL_ESC: '\\' ('0'..'3') ('0'..'7') ('0'..'7')
		| '\\' ('0'..'7') ('0'..'7')
		| '\\' ('0'..'7');

fragment UNICODE_ESC: '\\' 'u' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT;
