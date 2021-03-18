using System;
using System.Linq;
using Antlr4.Runtime.Tree;
using ImprovedSieve.Core.Models;
using ImprovedSieve.Core.Visitors;

namespace ImprovedSieve.Core
{
    public class SieveProcessor
    {
        private static readonly Lazy<SieveProcessor> Instance = new Lazy<SieveProcessor>(() => new SieveProcessor());

        public SieveOptions Options { get; private set; }

        public static SieveProcessor Current => Instance.Value;

        private SieveProcessor()
        {
            Options = SieveOptions.Defaults();
        }

        public void Init(SieveOptions options)
        {
            Options = options;
        }

        public IQueryable<T> CreateFilterExpression<T>(IQueryable<T> query, IParseTree filterTree)
        {
            var visitor = new SieveParser<T>(query, query.Expression);

            try
            {
                var expression = visitor.Visit(filterTree);

                return (IQueryable<T>) query.Provider.CreateQuery(expression);
            }
            catch (Exception ex)
            {
                ThrowException(ex);

                return query;
            }
        }

        public IQueryable<T> CreateSortExpression<T>(IQueryable<T> query, IParseTree sortByTree)
        {
            var visitor = new SieveParser<T>(query, query.Expression);

            try
            {
                var expression = visitor.Visit(sortByTree);

                return (IQueryable<T>) query.Provider.CreateQuery(expression);
            }
            catch (Exception ex)
            {
                ThrowException(ex);

                return query;
            }
        }

        public IQueryable<T> CreatePaginationExpression<T>(IQueryable<T> query, int? modelPage, int? modelPageSize)
        {
            var page = modelPage ?? 1;
            var pageSize = modelPageSize ?? Options.DefaultPageSize;
            var maxPageSize = Options.MaxPageSize > 0 ? Options.MaxPageSize : pageSize;

            if (pageSize > 0)
            {
                query = query.Skip((page - 1) * pageSize);
                query = query.Take(Math.Min(pageSize, maxPageSize));
            }

            return query;
        }

        private void ThrowException(Exception ex)
        {
            if (Options.ThrowExceptions)
            {
                throw new Exception("See inner exception", ex);
            }
        }
    }
}