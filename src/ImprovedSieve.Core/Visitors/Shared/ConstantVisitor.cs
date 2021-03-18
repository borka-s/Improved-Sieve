using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using ImprovedSieve.Core.Antlr;

namespace ImprovedSieve.Core.Visitors.Shared
{
    public class ConstantVisitor<TInput> : VisitorBase<TInput>
    {
        public Expression Visit(IQueryable query, Expression expression, SieveParser.ConstantContext context, Expression item = null)
        {
            SieveParser = new SieveParser<TInput>(query, expression, item);

            return ConvertNode(context);
        }

        private static Expression ConvertNode(SieveParser.ConstantContext context)
        {
            if (context.INT() != null)
            {
                return Expression.Constant(Convert.ToInt32(context.INT().GetText()));
            }

            if (context.BOOL() != null)
            {
                return Expression.Constant(Convert.ToBoolean(context.BOOL().GetText()));
            }

            if (context.STRING() != null)
            {
                var text = context.STRING().GetText().Trim('\'');
                text = text.Replace(@"\\", @"\");
                text = text.Replace(@"\b", "\b");
                text = text.Replace(@"\t", "\t");
                text = text.Replace(@"\n", "\n");
                text = text.Replace(@"\f", "\f");
                text = text.Replace(@"\r", "\r");
                text = text.Replace(@"\'", "'");
                text = text.Replace(@"''", "'");

                return Expression.Constant(text);
            }

            if (context.DATETIME() != null)
            {
                var dateText = context.DATETIME()
                    .GetText()
                    .Replace("'", string.Empty);

                return Expression.Constant(DateTime.Parse(dateText, null, DateTimeStyles.RoundtripKind));
            }

            if (context.LONG() != null)
            {
                return Expression.Constant(Convert.ToInt64(context.LONG().GetText().Replace("L", string.Empty)));
            }

            if (context.SINGLE() != null)
            {
                return Expression.Constant(Convert.ToSingle(context.SINGLE().GetText().Replace("f", string.Empty)));
            }

            if (context.DECIMAL() != null)
            {
                return Expression.Constant(Convert.ToDecimal(context.DECIMAL().GetText().Replace("m", string.Empty)));
            }

            if (context.DOUBLE() != null)
            {
                return Expression.Constant(Convert.ToDouble(context.DOUBLE().GetText().Replace("d", string.Empty)));
            }

            if (context.GUID() != null)
            {
                var guidText = context.GUID().GetText().Replace("guid'", string.Empty).Replace("'", string.Empty);

                return Expression.Constant(new Guid(guidText));
            }

            if (context.BYTE() != null)
            {
                return Expression.Constant(Convert.ToByte(context.BYTE().GetText().Replace("0x", string.Empty), 16));
            }

            if (context.NULL() != null)
            {
                return Expression.Constant(null);
            }

            return null;
        }
    }
}