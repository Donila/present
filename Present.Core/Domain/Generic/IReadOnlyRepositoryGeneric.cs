using System.Linq;

namespace Present.Core.Domain.Generic
{
    public interface IReadOnlyComplexKeyRepositoryGeneric<out TEntity>
       where TEntity : class
    {
        IQueryable<TEntity> Query { get; }
    }

    public interface IReadOnlyRepositoryGeneric<out TEntity, in TId> : IReadOnlyComplexKeyRepositoryGeneric<TEntity>
        where TEntity : EntityGeneric<TId>
    {
        TEntity Get(TId id);
    }
}
