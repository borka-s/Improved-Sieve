﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core
{
    public abstract class ParserBase
    {
        protected virtual SieveParser GetParser(string expression)
        {
            var inputStream = new AntlrInputStream(expression);
            var lexer = new SieveLexer(inputStream);

            var commonTokenStream = new CommonTokenStream(lexer);

            return new SieveParser(commonTokenStream);
        }

        public abstract IParseTree Parse(string expression);
    }
}