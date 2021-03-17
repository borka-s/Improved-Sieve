using System.Linq.Expressions;

namespace ImprovedSieve.Core.Visitors.NodeVisitors
{
    public class EqualsNode : TwoChildNode
    {
        public override Expression Visit(Expression leftExpression, Expression rightExpression)
        {
            if (leftExpression.Type == typeof(bool) && rightExpression.Type == typeof(bool) && rightExpression is ConstantExpression rightConstant)
            {
                if ((bool) rightConstant.Value)
                {
                    return leftExpression;
                }

                return Expression.Not(leftExpression);
            }

            if (rightExpression.Type == typeof(bool) && leftExpression.Type == typeof(bool) && leftExpression is ConstantExpression leftConstant)
            {
                if ((bool) leftConstant.Value)
                {
                    return rightExpression;
                }

                return Expression.Not(rightExpression);
            }

            NormalizeTypes(ref leftExpression, ref rightExpression);

            return ApplyEnsuringNullablesHaveValues(Expression.Equal, leftExpression, rightExpression);
        }
    }
}