using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;
using Present.Core.Domain.Generic;

namespace Present.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return source
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

    

        public static TEntity Update<TEntity>(this TEntity entity, IComplexKeyRepositoryGeneric<TEntity> repository)
            where TEntity : class
        {
            return repository.Update(entity);
        }

        public static TEntity Add<TEntity>(this TEntity entity, IComplexKeyRepositoryGeneric<TEntity> repository)
            where TEntity : class
        {
            return repository.Add(entity);
        }

        public static IEnumerable<TEntity> Add<TEntity>(this IEnumerable<TEntity> entityCollection,
                                                        IComplexKeyRepositoryGeneric<TEntity> repository)
            where TEntity : class
        {
            return entityCollection.Select(repository.Add).ToList();
        }

 

        public static IQueryable<T> Specify<T>(this IQueryable<T> source, Specification<T> specification)
        {
            return source.Where(specification.IsSatisfiedBy());
        }

   

        public static string GetPropertySymbol<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            return String.Join(".",
                               GetMembersOnPath(expression.Body as MemberExpression)
                                   .Select(m => m.Member.Name)
                                   .Reverse());

        }

        private static IEnumerable<MemberExpression> GetMembersOnPath(MemberExpression expression)
        {
            while (expression != null)
            {
                yield return expression;
                expression = expression.Expression as MemberExpression;
            }
        }

    }
}
