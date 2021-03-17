using System.Linq.Expressions;

namespace ImprovedSieve.Core.Visitors.NodeVisitors
{
    public abstract class TwoChildNode : TreeNodeBase
    {
        public abstract Expression Visit(Expression leftExpression, Expression rightExpression);
    }
}