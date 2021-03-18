using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;
using ImprovedSieve.Core.Visitors.Filters;
using ImprovedSieve.Core.Visitors.Shared;
using ImprovedSieve.Core.Visitors.SortBy;

namespace ImprovedSieve.Core.Visitors
{
    public class SieveParser<TInput> : SieveParserBaseVisitor<Expression>
    {
        private readonly IQueryable _query;

        public Expression Item { get; }

        public Expression Expression { get; }

        public SieveParser(IQueryable query, Expression expression, Expression item = null)
        {
            _query = query;
            Expression = expression;
            Item = item;
        }

        public override Expression VisitFilter(SieveParser.FilterContext context)
        {
            var visitor = new FilterVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitFilterExpression(SieveParser.FilterExpressionContext context)
        {
            var visitor = new FilterExpressionVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitOrExpression(SieveParser.OrExpressionContext context)
        {
            var visitor = new OrExpressionVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitAndExpression(SieveParser.AndExpressionContext context)
        {
            var visitor = new AndExpressionVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitBooleanExpression(SieveParser.BooleanExpressionContext context)
        {
            var visitor = new BooleanExpressionVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitAtom(SieveParser.AtomContext context)
        {
            var visitor = new AtomVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitPropertyName(SieveParser.PropertyNameContext context)
        {
            var visitor = new PropertyVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitSubPropertyName(SieveParser.SubPropertyNameContext context)
        {
            var visitor = new SubPropertyVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitIdentifierPart(SieveParser.IdentifierPartContext context)
        {
            var visitor = new IdentifierVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitConstant(SieveParser.ConstantContext context)
        {
            var visitor = new ConstantVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitSortBy(SieveParser.SortByContext context)
        {
            var visitor = new SortByVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitSortBylist(SieveParser.SortBylistContext context)
        {
            var visitor = new SortByListVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }

        public override Expression VisitSortPropertyName(SieveParser.SortPropertyNameContext context)
        {
            var visitor = new SortPropertyNameVisitor<TInput>();

            return visitor.Visit(_query, Expression, context, Item);
        }
    }
}