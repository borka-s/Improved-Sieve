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

        private SieveOptions _options;

        public static SieveProcessor Current => Instance.Value;

        private SieveProcessor()
        {
            _options = SieveOptions.Defaults();
        }

        public void Init(SieveOptions options)
        {
            _options = options;
        }

        public IQueryable<T> CreateFilterExpression<T>(IQueryable<T> query, IParseTree filterTree)
        {
            var visitor = new AutoParser<T>(query, query.Expression);

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
            var visitor = new AutoParser<T>(query, query.Expression);

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
            var pageSize = modelPageSize ?? _options.DefaultPageSize;
            var maxPageSize = _options.MaxPageSize > 0 ? _options.MaxPageSize : pageSize;

            if (pageSize > 0)
            {
                query = query.Skip((page - 1) * pageSize);
                query = query.Take(Math.Min(pageSize, maxPageSize));
            }

            return query;
        }

        private void ThrowException(Exception ex)
        {
            if (_options.ThrowExceptions)
            {
                throw new Exception("See inner exception", ex);
            }
        }
    }
}