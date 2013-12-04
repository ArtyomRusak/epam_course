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
            Property(e => e.ContactData.City).HasMaxLength(30);
            Property(e => e.ContactData.PhoneNumber).HasMaxLength(15);
            Property(e => e.Password).IsRequired();
            Property(e => e.PasswordSalt).IsRequired();
            Property(e => e.Email).HasMaxLength(40).IsRequired();
            Property(e => e.IsLogged).IsRequired();
            HasMany(e => e.Roles).WithMany(e => e.Users);
            HasMany(e => e.Lots).WithRequired(e => e.Owner).HasForeignKey(e => e.OwnerId);
        }
    }
}
