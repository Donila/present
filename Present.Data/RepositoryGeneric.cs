using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LinqSpecs;
using Present.Core.Domain.Generic;
using Present.Core.Extensions;

namespace Present.Data
{
    public class ComplexKeyRepositoryGeneric<TEntity> : IComplexKeyRepositoryGeneric<TEntity> where TEntity : class
    {
        public ComplexKeyRepositoryGeneric(DbContext dataContext)
        {
            DataContext = dataContext;
            Dbset = DataContext.Set<TEntity>();
            Query = Dbset;
        }

        public ComplexKeyRepositoryGeneric(DbContext dataContext, string includePath)
            : this(dataContext)
        {
            Query = Query.Include(includePath);
        }

        public ComplexKeyRepositoryGeneric(DbContext dataContext, params string[] includePath)
            : this(dataContext)
        {
            Query = includePath.Aggregate(Query, (current, path) => current.Include(path));
        }

        public ComplexKeyRepositoryGeneric(DbContext dataContext, string[] includePaths, params Specification<TEntity>[] includeSpecs)
            : this(dataContext, includePaths)
        {
            Query = includeSpecs.Aggregate(Query, (current, spec) => current.Specify(spec));
        }

        protected DbContext DataContext { get; private set; }
        protected IDbSet<TEntity> Dbset { get; private set; }
        public IQueryable<TEntity> Query { get; private set; }

        public TEntity Update(TEntity entity)
        {
            if (entity == null) return default(TEntity);
            Dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null) return default(TEntity);
            Dbset.Add(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) return;
            Dbset.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            var objects = Dbset.Where(@where).AsEnumerable();
            foreach (var obj in objects)
            {
                Dbset.Remove(obj);
            }
        }
    }

    public class RepositoryGeneric<TEntity, TId> : ComplexKeyRepositoryGeneric<TEntity>, IRepositoryGeneric<TEntity, TId> where TEntity : EntityGeneric<TId>
    {
        private readonly string _keyName;

        public RepositoryGeneric(DbContext dataContext, string keyName)
            : base(dataContext)
        {
            _keyName = keyName;
        }
        public RepositoryGeneric(DbContext dataContext, string keyName, string includePath)
            : base(dataContext, includePath)
        {
            _keyName = keyName;

        }
        public RepositoryGeneric(DbContext dataContext, string keyName, params string[] includePath)
            : base(dataContext, includePath)
        {
            _keyName = keyName;

        }
        public RepositoryGeneric(DbContext dataContext, string keyName, string[] includePaths, params Specification<TEntity>[] includeSpecs)
            : base(dataContext, includePaths, includeSpecs)
        {
            _keyName = keyName;

        }

        public TEntity Get(TId id)
        {
            return Query.FirstOrDefault(x => x.Id.Equals(id));
        }

        //public TId NewId
        //{
        //    get
        //    {
        //        return DataContext.Database.SqlQuery<TId>(
        //                @"DECLARE @RET_ID INT; EXEC @RET_ID = dbo.usp_GetNextID @KeyName; SELECT @RET_ID INT",
        //                new SqlParameter
        //                {
        //                    ParameterName = "@KeyName",
        //                    Value = _keyName
        //                }).Single();
        //    }
        //}

        //public override TEntity Add(TEntity entity)
        //{
        //    if (entity == null) return default(TEntity);
        //    if (entity.Id.Equals(default(TId)))
        //    {
        //        entity.Id = NewId;
        //    }
        //    return base.Add(entity);
        //}
    }
}
