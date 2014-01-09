using System.Data.Entity.ModelConfiguration;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.EFData.EFContext.Mappings.Membership
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(e => e.Id);
            Property(e => e.UserName).HasMaxLength(30).IsRequired();
            Property(e => e.Password).IsRequired();
            Property(e => e.PasswordSalt).IsRequired();
            Property(e => e.RegisterDate).IsRequired();
            Property(e => e.Email).HasMaxLength(40).IsRequired();
            HasMany(e => e.Lots).WithRequired(e => e.Owner).HasForeignKey(e => e.OwnerId);
            HasMany(e => e.Roles).WithMany(e => e.Users);
        }
    }
}
