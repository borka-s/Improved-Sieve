using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.Filters
{
    public class FilterVisitor<TInput> : VisitorBase<TInput>
    {
        public Expression Visit(IQueryable query, Expression expression, AutoFilterParser.FilterContext context, Expression item = null)
        {
            var parameter = Expression.Parameter(typeof(TInput), "x");

            AutoParser = new AutoParser<TInput>(query, expression, parameter);
            var body = VisitChildren(context);
            var lambda = Expression.Lambda(body, parameter);

            return Expression.Call(typeof(Queryable), "Where", new[] { query.ElementType }, query.Expression, lambda);
        }
    }
}