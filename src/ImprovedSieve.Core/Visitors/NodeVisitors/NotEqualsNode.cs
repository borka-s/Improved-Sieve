using System.Linq.Expressions;

namespace ImprovedSieve.Core.Visitors.NodeVisitors
{
    public class NotEqualsNode : TwoChildNode
    {
        public override Expression Visit(Expression leftExpression, Expression rightExpression)
        {
            NormalizeTypes(ref leftExpression, ref rightExpression);

            return ApplyWithNullAsValidAlternative(Expression.NotEqual, leftExpression, rightExpression);
        }
    }
}