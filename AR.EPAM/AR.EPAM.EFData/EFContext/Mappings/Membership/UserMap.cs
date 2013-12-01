using System.Data.Entity.ModelConfiguration;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.EFData.EFContext.Mappings.Membership
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(e => e.Id);
            Property(e => e.UserName).IsRequired().HasMaxLength(30);
            Property(e => e.ContactData.City).IsRequired().HasMaxLength(30);
            Property(e => e.Password).HasMaxLength(16).IsRequired();
            Property(e => e.Email).HasMaxLength(40).IsRequired();
            HasMany(e => e.Roles).WithMany(e => e.Users);
            HasMany(e => e.Lots).WithRequired(e => e.Owner).HasForeignKey(e => e.OwnerId);
        }
    }
}
