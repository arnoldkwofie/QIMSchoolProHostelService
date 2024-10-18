using Qface.Application.Shared.Common.Models;
using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Application.Shared.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            try
            {
                return condition ? query.Where(predicate) : query;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static IQueryable<AuditableEntity<T>> WhereIsActive<T>(this IQueryable<AuditableEntity<T>> query, Expression<Func<AuditableEntity<T>, bool>> predicate)
        {
            try
            {
                return query.Where(a => a.Audit.EntityStatus == EntityStatus.Normal).Where(predicate);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static IQueryable<AuditableEntity<T>> WhereIsActive<T>(this IQueryable<AuditableEntity<T>> query)
        {
            try
            {
                return query.Where(a => a.Audit.EntityStatus == EntityStatus.Normal);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static IQueryable<T> TakeIf<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy, bool condition, int limit, bool orderByDescending = true)
        {
            // It is necessary sort items before it
            query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            return condition
                ? query.Take(limit)
                : query;
        }

        public static IQueryable<T> PageBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy,
            int page,
            int pageSize,
            bool? orderByDescending = true)
        {
            const int defaultPageNumber = 1;

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            // Check if the page number is greater then zero - otherwise use default page number
            if (page <= 0)
            {
                page = defaultPageNumber;
            }


            if (orderByDescending != null)
            {
                // It is necessary sort items before it
                query = orderByDescending ?? false ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }

            int skip = (page - 1) * pageSize;
            return query.Skip(skip).Take(pageSize);
        }

        public static IQueryable<T> PageBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy,
            PaginatedCommand paginated)
        {
            const int defaultPageNumber = 1;

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            // Check if the page number is greater then zero - otherwise use default page number

            if (paginated.PageNumber <= 0)
            {
                paginated.PageNumber = defaultPageNumber;
            }


            //if (orderByDescending != null)
            //{
            //	// It is necessary sort items before it
            //	query = orderByDescending ?? false ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            //}

            //int skip = (page - 1) * pageSize;
            return query.Skip(paginated.Skip).Take(paginated.PageSize);
        }





    }
}
