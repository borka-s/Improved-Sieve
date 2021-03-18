parser grammar SieveParser;

@parser::header {#pragma warning disable 3021}

options { tokenVocab=SieveLexer; }

// Filters
filter: filterExpression[false];

filterExpression[bool subquery]: orExpression[subquery] (OR orExpression[subquery])*;

orExpression[bool subquery]: andExpression[subquery] (',' andExpression[subquery])*;

andExpression[bool subquery]: ('(' filterExpression[subquery] ')' | booleanExpression[subquery])*;

booleanExpression[bool subquery]
	: NOT? atom1=atom[subquery]
	   (op=(EQUALS
	    | NOTEQUALS
	    | GREATERTHAN
	    | LESSTHAN
	    | GREATERTHANOREQUAL
	    | LESSTHANOREQUAL
	    | CONTAINS
	    | STARTSWITH
	    | ENDSWITH
        | NOTCONTAINS
	    | NOTSTARTSWITH
	    | NOTENDSWITH
        | CASEIGNORECONTAINS
        | CASEIGNORESTARTSWITH
        | CASEIGNOREEQUALS
        | CASEIGNORENOTEQUALS
        | CASEIGNORENOTCONTAINS
        | CASEIGNOREENDSWITH
        | CASEIGNORENOTSTARTSWITH
        | CASEIGNORENOTENDSWITH
        )
	  atom2=atom[subquery])?;

// Order
sortBy: sortBylist;

sortBylist: sortPropertyName (',' sortPropertyName)*;

sortPropertyName: NOT? DESC? propertyName[false];

// Variables
atom[bool subquery]: (constant (OR constant)*) | propertyName[subquery];
propertyName[bool subquery]: (identifierPart[subquery]) ('.' next=subPropertyName[false])?;
subPropertyName[bool subquery]: propertyName[false];
identifierPart[bool subquery] :	(id=IDENTIFIER | DYNAMICIDENTIFIER);
constant: (INT | BOOL | STRING | DATETIME | LONG | SINGLE | DECIMAL | DOUBLE | GUID | BYTE | NULL);
