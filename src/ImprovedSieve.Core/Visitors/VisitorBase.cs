using System;
using System.Linq.Expressions;
using Antlr4.Runtime.Tree;

namespace ImprovedSieve.Core.Visitors
{
    public abstract class VisitorBase<TInput> : AbstractParseTreeVisitor<Expression>
    {
        protected AutoParser<TInput> AutoParser;

        protected bool IsFirstChild { get; private set; }

        public IParseTree Child { get; private set; }

        public override Expression VisitChildren(IRuleNode node)
        {
            var result = DefaultResult;
            var childCount = node.ChildCount;

            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                IsFirstChild = i == 0;
                Child = node.GetChild(i);
                var nextResult = Child.Accept(AutoParser);
                result = AggregateResult(result, nextResult);
            }

            return result;
        }

        protected static Expression CastIfNeeded(Expression expression, Type type)
        {
            var converted = expression;

            if (!type.IsAssignableFrom(expression.Type))
            {
                var convertToType = Configuration.TypeConversionMap(expression.Type, type);
                converted = Expression.Convert(expression, convertToType);
            }

            return converted;
        }

        private static Expression MapAndCast(Expression from, Expression to)
        {
            var mapped = Configuration.TypeConversionMap(from.Type, to.Type);

            if (mapped != from.Type)
            {
                from = CastIfNeeded(from, mapped);
            }

            return CastIfNeeded(from, to.Type);
        }
    }
}