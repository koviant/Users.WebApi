using System.Data.Entity;

using Users.DAL.Base;

namespace Users.DAL.EF
{
    using ConfigConstants;

    public class UsersContext : DbContext
    {
        public UsersContext() : base($"name={ConfigConstants.DatabaseName}")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<UserDal> Users { get; set; }

        public DbSet<AddressDal> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
