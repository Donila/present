using System;
using System.Linq.Expressions;

namespace Present.Core.Domain.Generic
{
    public interface IComplexKeyRepositoryGeneric<TEntity> : IReadOnlyComplexKeyRepositoryGeneric<TEntity>
        where TEntity : class
    {
        TEntity Update(TEntity entity);
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);
    }

    public interface IRepositoryGeneric<TEntity, TId> : IReadOnlyRepositoryGeneric<TEntity, TId>, IComplexKeyRepositoryGeneric<TEntity> where TEntity : EntityGeneric<TId>
    {
      //  TId NewId { get; }
    }
}
