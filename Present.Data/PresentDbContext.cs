using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Present.Data.Mapping.Users;

namespace Present.Data
{
   public class PresentDbContext : DbContext
    {
           public PresentDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

           public PresentDbContext(string connString, int commandTimeout)
            : base(connString)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = commandTimeout;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           

         

            MapUsersRelatedDomainObjects(modelBuilder);


        }
        public void MapUsersRelatedDomainObjects(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new UsersDbConfiguration());

        }

    }
}
