using System.Data.Entity.ModelConfiguration;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.EFData.EFContext.Mappings.Membership
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Name).IsRequired().HasMaxLength(30);
            HasMany(e => e.Users).WithMany(e => e.Roles);
        }
    }
}
