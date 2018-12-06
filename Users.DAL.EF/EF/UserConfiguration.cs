using System.Data.Entity.ModelConfiguration;

using Users.DAL.Base;

namespace Users.DAL.EF
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;

    using ConfigConstants;

    public class UserConfiguration : EntityTypeConfiguration<UserDal>
    {
        public UserConfiguration()
        {
            ToTable(ConfigConstants.UsersTableName);
            HasKey(u => u.Id);
            Property(u => u.Birthdate).IsRequired().HasColumnType("Date");
            Property(u => u.FirstName).IsRequired().IsUnicode().HasMaxLength(ConfigConstants.UserFirstNameLength).IsVariableLength();
            Property(u => u.LastName).IsRequired().IsUnicode().HasMaxLength(ConfigConstants.UserLastNameLength).IsVariableLength();

            // Unique string field. Found at https://stackoverflow.com/questions/10614575/entity-framework-code-first-unique-column.
            Property(u => u.LoginName).IsRequired().IsUnicode().HasMaxLength(ConfigConstants.UserLoginNameLength).IsVariableLength()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));

            Property(u => u.Password).IsRequired().IsUnicode().HasMaxLength(ConfigConstants.UserPasswordLength).IsVariableLength();
            HasMany(u => u.Addresses).WithRequired(a => a.User);
        }
    }
}