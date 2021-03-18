using System.Linq.Expressions;
using Antlr4.Runtime.Tree;

namespace ImprovedSieve.Core.Visitors
{
    public abstract class VisitorBase<TInput> : AbstractParseTreeVisitor<Expression>
    {
        protected SieveParser<TInput> SieveParser;

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
                var nextResult = Child.Accept(SieveParser);
                result = AggregateResult(result, nextResult);
            }

            return result;
        }
    }
}