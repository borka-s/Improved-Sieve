using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.Shared
{
    public class SubPropertyVisitor<TInput> : VisitorBase<TInput>
    {
        public Expression Visit(IQueryable query, Expression expression, AutoFilterParser.SubPropertyNameContext context, Expression item = null)
        {
            AutoParser = new AutoParser<TInput>(query, expression, item);

            return VisitChildren(context);
        }
    }
}