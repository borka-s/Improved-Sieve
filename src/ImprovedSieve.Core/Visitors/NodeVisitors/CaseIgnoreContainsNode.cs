using System;
using System.Linq.Expressions;

namespace ImprovedSieve.Core.Visitors.NodeVisitors
{
    public class CaseIgnoreContainsNode : TwoChildNode
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

            var comparisonType = Expression.Constant(StringComparison.InvariantCultureIgnoreCase);

            return Expression.Call(leftExpression, Constants.ExpressionMethods.Contains, null, rightExpression, comparisonType);
        }
    }
}