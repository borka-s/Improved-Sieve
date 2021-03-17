using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.Filters
{
    public class AndExpressionVisitor<TInput> : VisitorBase<TInput>
    {
        protected override Expression AggregateResult(Expression aggregate, Expression nextResult)
        {
            if (aggregate == null)
            {
                return nextResult;
            }

            if (nextResult == null)
            {
                return aggregate;
            }

            return null;
        }

        public Expression Visit(IQueryable query, Expression expression, AutoFilterParser.AndExpressionContext context, Expression item = null)
        {
            AutoParser = new AutoParser<TInput>(query, expression, item);

            return VisitChildren(context);
        }
    }
}