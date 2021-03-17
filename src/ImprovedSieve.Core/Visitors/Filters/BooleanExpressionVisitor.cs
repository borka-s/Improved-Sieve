using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;
using ImprovedSieve.Core.Visitors.NodeVisitors;

namespace ImprovedSieve.Core.Visitors.Filters
{
    public class BooleanExpressionVisitor<TInput> : VisitorBase<TInput>
    {
        public Expression Visit(IQueryable query, Expression expression, AutoFilterParser.BooleanExpressionContext context, Expression item = null)
        {
            AutoParser = new AutoParser<TInput>(query, expression, item);

            var leftExpression = VisitChildren(context.atom1);
            var rightExpression = VisitChildren(context.atom2);

            if (context.EQUALS() != null)
            {
                return new EqualsNode().Visit(leftExpression, rightExpression);
            }

            if (context.NOTEQUALS() != null)
            {
                return new NotEqualsNode().Visit(leftExpression, rightExpression);
            }

            if (context.GREATERTHAN() != null)
            {
                return new GreaterThanNode().Visit(leftExpression, rightExpression);
            }

            if (context.LESSTHAN() != null)
            {
                return new LessThanNode().Visit(leftExpression, rightExpression);
            }

            if (context.GREATERTHANOREQUAL() != null)
            {
                return new GreaterThanOrEqualNode().Visit(leftExpression, rightExpression);
            }

            if (context.LESSTHANOREQUAL() != null)
            {
                return new LessThanOrEqualNode().Visit(leftExpression, rightExpression);
            }

            if (context.CONTAINS() != null)
            {
                return new ContainsNode().Visit(leftExpression, rightExpression);
            }

            if (context.STARTSWITH() != null)
            {
                return new StartsWithNode().Visit(leftExpression, rightExpression);
            }

            if (context.ENDSWITH() != null)
            {
                return new EndsWithNode().Visit(leftExpression, rightExpression);
            }

            if (context.NOTCONTAINS() != null)
            {
                return Expression.Not(new ContainsNode().Visit(leftExpression, rightExpression));
            }

            if (context.NOTSTARTSWITH() != null)
            {
                return Expression.Not(new StartsWithNode().Visit(leftExpression, rightExpression));
            }

            if (context.NOTENDSWITH() != null)
            {
                return Expression.Not(new EndsWithNode().Visit(leftExpression, rightExpression));
            }

            if (context.CASEIGNORECONTAINS() != null)
            {
                return new CaseIgnoreContainsNode().Visit(leftExpression, rightExpression);
            }

            if (context.CASEIGNORESTARTSWITH() != null)
            {
                return new CaseIgnoreStartsWithNode().Visit(leftExpression, rightExpression);
            }

            if (context.CASEIGNOREEQUALS() != null)
            {
                return new CaseIgnoreEqualsNode().Visit(leftExpression, rightExpression);
            }

            if (context.CASEIGNORENOTEQUALS() != null)
            {
                return Expression.Not(new CaseIgnoreEqualsNode().Visit(leftExpression, rightExpression));
            }

            if (context.CASEIGNORENOTCONTAINS() != null)
            {
                return Expression.Not(new CaseIgnoreContainsNode().Visit(leftExpression, rightExpression));
            }

            if (context.CASEIGNOREENDSWITH() != null)
            {
                return new CaseIgnoreEndsWithNode().Visit(leftExpression, rightExpression);
            }

            if (context.CASEIGNORENOTSTARTSWITH() != null)
            {
                return Expression.Not(new CaseIgnoreStartsWithNode().Visit(leftExpression, rightExpression));
            }

            if (context.CASEIGNORENOTENDSWITH() != null)
            {
                return Expression.Not(new CaseIgnoreEndsWithNode().Visit(leftExpression, rightExpression));
            }

            return null;
        }
    }
}