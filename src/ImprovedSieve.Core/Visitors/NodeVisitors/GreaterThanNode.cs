using System.Linq.Expressions;

namespace ImprovedSieve.Core.Visitors.NodeVisitors
{
    public class GreaterThanNode : TwoChildNode
    {
        public override Expression Visit(Expression leftExpression, Expression rightExpression)
        {
            NormalizeTypes(ref leftExpression, ref rightExpression);

            return ApplyEnsuringNullablesHaveValues(Expression.GreaterThan, leftExpression, rightExpression);
        }
    }
}