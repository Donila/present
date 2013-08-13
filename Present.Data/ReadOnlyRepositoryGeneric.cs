using System.Data.Entity;
using System.Linq;
using LinqSpecs;
using Present.Core.Domain.Generic;
using Present.Core.Extensions;

namespace Present.Data
{
    public class ReadOnlyRepositoryGeneric<TEntity, TId> : IReadOnlyRepositoryGeneric<TEntity, TId> where TEntity : EntityGeneric<TId>
    {
        protected DbContext DataContext { get; private set; }
        protected IDbSet<TEntity> Dbset { get; private set; }
        public IQueryable<TEntity> Query { get; private set; }

        public ReadOnlyRepositoryGeneric(DbContext dataContext)
        {
            DataContext = dataContext;
            Dbset = DataContext.Set<TEntity>();
            Query = Dbset;
        }

        public ReadOnlyRepositoryGeneric(DbContext dataContext, params string[] includePath)
            : this(dataContext)
        {
            Query = includePath.Aggregate(Query, (current, path) => current.Include(path));
        }

        public ReadOnlyRepositoryGeneric(DbContext dataContext, string[] includePaths, params Specification<TEntity>[] includeSpecs)
            : this(dataContext, includePaths)
        {
            Query = includeSpecs.Aggregate(Query, (current, spec) => current.Specify(spec));
        }

        public TEntity Get(TId id)
        {
            return Query.FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
