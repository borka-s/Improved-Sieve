using System;
using System.Linq.Expressions;

namespace ImprovedSieve.Core.Visitors.NodeVisitors
{
    public abstract class TreeNodeBase
    {
        protected static Expression ApplyEnsuringNullablesHaveValues(Func<Expression, Expression, Expression> produces, Expression leftExpression, Expression rightExpression)
        {
            var leftExpressionIsNullable = Nullable.GetUnderlyingType(leftExpression.Type) != null;
            var rightExpressionIsNullable = Nullable.GetUnderlyingType(rightExpression.Type) != null;

            if (leftExpressionIsNullable && !rightExpressionIsNullable)
            {
                return Expression.AndAlso(
                    Expression.NotEqual(leftExpression, Expression.Constant(null)),
                    produces(Expression.PropertyOrField(leftExpression, "Value"), rightExpression));
            }

            if (rightExpressionIsNullable && !leftExpressionIsNullable)
            {
                return Expression.AndAlso(
                    Expression.NotEqual(rightExpression, Expression.Constant(null)),
                    produces(leftExpression, Expression.PropertyOrField(rightExpression, "Value")));
            }

            return produces(leftExpression, rightExpression);
        }

        protected static void NormalizeTypes(ref Expression leftSide, ref Expression rightSide)
        {
            var rightSideIsConstant = rightSide is ConstantExpression;
            var leftSideIsConstant = leftSide is ConstantExpression;

            switch (rightSideIsConstant)
            {
                case true when leftSideIsConstant:
                    return;
                // If we are comparing to an object try to cast it to the same type as the constant
                case true when leftSide.Type == typeof(object):
                    leftSide = MapAndCast(leftSide, rightSide);

                    break;
                case true:
                    rightSide = MapAndCast(rightSide, leftSide);

                    break;
            }

            if (leftSideIsConstant)
            {
                // If we are comparing to an object try to cast it to the same type as the constant
                if (rightSide.Type == typeof(object))
                {
                    rightSide = MapAndCast(rightSide, leftSide);
                }
                else
                {
                    leftSide = MapAndCast(leftSide, rightSide);
                }
            }
        }

        protected static Expression ApplyWithNullAsValidAlternative(Func<Expression, Expression, Expression> produces, Expression leftExpression, Expression rightExpression)
        {
            var leftExpressionIsNullable = Nullable.GetUnderlyingType(leftExpression.Type) != null;
            var rightExpressionIsNullable = Nullable.GetUnderlyingType(rightExpression.Type) != null;

            if (leftExpressionIsNullable && !rightExpressionIsNullable)
            {
                return Expression.OrElse(
                    Expression.Equal(leftExpression, Expression.Constant(null)),
                    produces(Expression.PropertyOrField(leftExpression, "Value"), rightExpression));
            }

            if (rightExpressionIsNullable && !leftExpressionIsNullable)
            {
                return Expression.OrElse(
                    Expression.Equal(rightExpression, Expression.Constant(null)),
                    produces(leftExpression, Expression.PropertyOrField(rightExpression, "Value")));
            }

            return produces(leftExpression, rightExpression);
        }

        protected static Expression CastIfNeeded(Expression expression, Type type)
        {
            var converted = expression;

            if (!type.IsAssignableFrom(expression.Type))
            {
                var convertToType = Configuration.TypeConversionMap(expression.Type, type);
                converted = Expression.Convert(expression, convertToType);
            }

            return converted;
        }

        private static Expression MapAndCast(Expression from, Expression to)
        {
            var mapped = Configuration.TypeConversionMap(from.Type, to.Type);

            if (mapped != from.Type)
            {
                from = CastIfNeeded(from, mapped);
            }

            return CastIfNeeded(from, to.Type);
        }
    }
}