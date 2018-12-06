using System.Data.Entity.ModelConfiguration;

using Users.DAL.Base;

namespace Users.DAL.EF
{
    using ConfigConstants;

    public class AddressConfiguration : EntityTypeConfiguration<AddressDal>
    {
        public AddressConfiguration()
        {
            ToTable(ConfigConstants.AddressesTableName);
            HasKey(a => a.Id);
            Property(a => a.Description).IsRequired().IsUnicode().HasMaxLength(ConfigConstants.AddressDescriptionLength).IsVariableLength();
            Property(a => a.Value).IsRequired().IsUnicode().HasMaxLength(ConfigConstants.AddressValueLength).IsVariableLength();
            HasRequired(a => a.User).WithMany(u => u.Addresses).WillCascadeOnDelete(true);
        }
    }
}