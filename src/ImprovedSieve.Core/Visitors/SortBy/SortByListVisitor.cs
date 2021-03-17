using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.SortBy
{
    public class SortByListVisitor<TInput> : VisitorBase<TInput>
    {
        protected override Expression AggregateResult(Expression aggregate, Expression nextResult)
        {
            if (nextResult == null)
            {
                return aggregate;
            }

            var sortPropertyNameContext = (AutoFilterParser.SortPropertyNameContext) Child;
            var desc = sortPropertyNameContext.DESC() != null;

            var command = desc
                ? IsFirstChild ? "OrderByDescending" : "ThenByDescending"
                : IsFirstChild
                    ? "OrderBy"
                    : "ThenBy";


            var lambda = Expression.Lambda(nextResult, AutoParser.Item as ParameterExpression);

            var result = Expression.Call(typeof(Queryable), command, new[] { typeof(TInput), lambda.ReturnType }, aggregate ?? AutoParser.Expression, Expression.Quote(lambda));

            return result;
        }

        public Expression Visit(IQueryable query, Expression expression, AutoFilterParser.SortBylistContext context, Expression item = null)
        {
            AutoParser = new AutoParser<TInput>(query, expression, item);
            var result = VisitChildren(context);

            return result;
        }
    }
}