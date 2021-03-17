using System.Linq.Expressions;

namespace ImprovedSieve.Core.Visitors.NodeVisitors
{
    public class StartsWithNode : TwoChildNode
    {
        public override Expression Visit(Expression leftExpression, Expression rightExpression)
        {
            if (!typeof(string).IsAssignableFrom(leftExpression.Type))
            {
                leftExpression = Expression.Convert(leftExpression, typeof(string));
            }

            if (!typeof(string).IsAssignableFrom(rightExpression.Type))
            {
                rightExpression = Expression.Convert(rightExpression, typeof(string));
            }

            return Expression.Call(leftExpression, Constants.ExpressionMethods.StartsWith, null, rightExpression);
        }
    }
}