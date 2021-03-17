using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;
using ImprovedSieve.Core.Visitors.Filters;
using ImprovedSieve.Core.Visitors.Shared;
using ImprovedSieve.Core.Visitors.SortBy;

namespace ImprovedSieve.Core.Visitors
{
    public class AutoParser<TInput> : AutoFilterParserBaseVisitor<Expression>
    {
        private readonly IQueryable _query;

        public Expression Item { get; }

        public Expression Expression { get; }

        public AutoParser(IQueryable query, Expression expression, Expression item = null)
        {
            _query = query;
            Expression = expression;
            Item = item;
        }

        public override Expression VisitFilter(AutoFilterParser.FilterContext context)
        {
            var visitor = new FilterVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitFilterExpression(AutoFilterParser.FilterExpressionContext context)
        {
            var visitor = new FilterExpressionVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitOrExpression(AutoFilterParser.OrExpressionContext context)
        {
            var visitor = new OrExpressionVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitAndExpression(AutoFilterParser.AndExpressionContext context)
        {
            var visitor = new AndExpressionVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitBooleanExpression(AutoFilterParser.BooleanExpressionContext context)
        {
            var visitor = new BooleanExpressionVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitAtom(AutoFilterParser.AtomContext context)
        {
            var visitor = new AtomVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitPropertyName(AutoFilterParser.PropertyNameContext context)
        {
            var visitor = new PropertyVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitSubPropertyName(AutoFilterParser.SubPropertyNameContext context)
        {
            var visitor = new SubPropertyVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitIdentifierPart(AutoFilterParser.IdentifierPartContext context)
        {
            var visitor = new IdentifierVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitConstant(AutoFilterParser.ConstantContext context)
        {
            var visitor = new ConstantVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitSortBy(AutoFilterParser.SortByContext context)
        {
            var visitor = new SortByVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitSortBylist(AutoFilterParser.SortBylistContext context)
        {
            var visitor = new SortByListVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitSortPropertyName(AutoFilterParser.SortPropertyNameContext context)
        {
            var visitor = new SortPropertyNameVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }
    }
}