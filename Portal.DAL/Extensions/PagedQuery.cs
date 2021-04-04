using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using His.Reception.DTO;
using Portal.DTO;

namespace Portal.DAL.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IQueryableExtensions
    {
        public static bool HasValidPaging(this IPaging paging) => paging != null && paging.PageSize > 0 && paging.PageNumber > 0;
   
        public static IQueryable<T> ToPagedQuery<T>(this IQueryable<T> query, IPaging paging)
        {
            return paging != null && paging.HasValidPaging()?
                query.Skip(paging.PageSize * (paging.PageNumber - 1)).Take(paging.PageSize)
                : query;
        }

        public static IQueryable<T> If<T>(this IQueryable<T> sourceQuery, bool condition, Func<IQueryable<T>, IQueryable<T>> query)
            => condition ? query(sourceQuery) : sourceQuery;

        public static IQueryable<T> WhereEquals<T>(this IQueryable<T> source, string propertyName, object value)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null) return null;

            var parameter = Expression.Parameter(typeof(T), "item");
            var whereProperty = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(value);
            var condition = Expression.Equal(whereProperty, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
            return source.Where(lambda);
        }
    }
}
