using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;
using ImprovedSieve.Core.Extensions;

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

            var sortPropertyNameContext = (SieveParser.SortPropertyNameContext) Child;
            var desc = sortPropertyNameContext.DESC() != null;

            var command = desc
                ? IsFirstChild ? "OrderByDescending" : "ThenByDescending"
                : IsFirstChild
                    ? "OrderBy"
                    : "ThenBy";

            var ignoreSortingNulls = SieveProcessor.Current.Options.IgnoreSortingNulls || sortPropertyNameContext.NOT() != null;

            var lambda = ApplyNullCheckExpression(nextResult, SieveParser.Item, ignoreSortingNulls);

            var result = Expression.Call(typeof(Queryable), command, new[] { typeof(TInput), lambda.ReturnType }, aggregate ?? SieveParser.Expression, Expression.Quote(lambda));

            return result;
        }

        private LambdaExpression ApplyNullCheckExpression(Expression property, Expression parameter, bool ignoreSortingNulls)
        {
            var propertyMemberAccess = (MemberExpression) property;
            Expression nullCheck = null;

            var propertyValue = parameter;

            foreach (var name in GetPropertyNames(propertyMemberAccess).Reverse())
            {
                propertyValue = Expression.PropertyOrField(propertyValue, name);

                if (propertyValue.Type.IsNullable() && !ignoreSortingNulls)
                {
                    nullCheck = GenerateOrderNullCheckExpression(propertyValue, nullCheck);
                }
            }

            var expression = nullCheck == null
                ? propertyValue
                : Expression.Condition(nullCheck, Expression.Default(propertyValue.Type), propertyValue);

            var converted = Expression.Convert(expression, typeof(object));

            return Expression.Lambda(converted, SieveParser.Item as ParameterExpression);
        }

        private static Expression GenerateOrderNullCheckExpression(Expression propertyValue, Expression nullCheckExpression)
        {
            return nullCheckExpression == null
                ? Expression.Equal(propertyValue, Expression.Default(propertyValue.Type))
                : Expression.OrElse(nullCheckExpression, Expression.Equal(propertyValue, Expression.Default(propertyValue.Type)));
        }

        private IEnumerable<string> GetPropertyNames(MemberExpression body)
        {
            while (body != null)
            {
                yield return body.Member.Name;

                var inner = body.Expression;

                switch (inner.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        body = inner as MemberExpression;

                        break;
                    default:
                        body = null;

                        break;
                }
            }
        }

        public Expression Visit(IQueryable query, Expression expression, SieveParser.SortBylistContext context, Expression item = null)
        {
            SieveParser = new SieveParser<TInput>(query, expression, item);
            var result = VisitChildren(context);

            return result;
        }
    }
}