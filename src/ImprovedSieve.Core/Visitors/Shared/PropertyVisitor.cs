using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.Shared
{
    public class PropertyVisitor<TInput> : VisitorBase<TInput>
    {
        public Expression Visit(IQueryable query, Expression expression, SieveParser.PropertyNameContext context, Expression item = null)
        {
            var property = Expression.PropertyOrField(item, context.identifierPart().GetText());

            SieveParser = new SieveParser<TInput>(query, expression, property);

            return VisitChildren(context);
        }
    }
}