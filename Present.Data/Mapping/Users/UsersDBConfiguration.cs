using System.Data.Entity.ModelConfiguration;
using Present.Domain.Users;

namespace Present.Data.Mapping.Users
{
    public class UsersDbConfiguration : EntityTypeConfiguration<User>
    {
        public UsersDbConfiguration()
        {
            HasKey(x => x.Id);
            ToTable("Users");
            HasKey(x => x.Id)
                .Property(x => x.Id).HasColumnName("UserId");
        }
    }
}


