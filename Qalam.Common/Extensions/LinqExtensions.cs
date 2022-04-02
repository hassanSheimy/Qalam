using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Qalam.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<TSource> IncludeMultiple<TSource>(this IQueryable<TSource> query, string[] includes) where TSource : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, 
                    (current, include) => current.Include(include));
            }

            return query;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<T> Set<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
                yield return item;
            }
        }
    }
}
