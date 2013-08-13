using System.Data.Entity;
using Present.Core.Domain;

namespace Present.Data
{
   public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
    }
}
