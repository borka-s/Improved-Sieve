using System.Linq;
using ImprovedSieve.Core.Models;

namespace ImprovedSieve.Core.Extensions
{
    public static class QueriableExtensions
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, SieveModel model)
        {
            return SieveProcessor.Current.CreateFilterExpression(query, model.FilterTree);
        }

        public static IQueryable<T> ApplySortBy<T>(this IQueryable<T> query, SieveModel model)
        {
            return SieveProcessor.Current.CreateSortExpression(query, model.SortTree);
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, SieveModel model)
        {
            return SieveProcessor.Current.CreatePaginationExpression(query, model.Page, model.PageSize);
        }

        public static IQueryable<T> ApplySieve<T>(this IQueryable<T> query, SieveModel model)
        {
            return query.ApplyFilters(model).ApplySortBy(model).ApplyPagination(model);
        }
    }
}