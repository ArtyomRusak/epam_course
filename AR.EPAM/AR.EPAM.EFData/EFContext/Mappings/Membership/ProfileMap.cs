using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.EFData.EFContext.Mappings.Membership
{
    public class ProfileMap : EntityTypeConfiguration<Profile>
    {
        public ProfileMap()
        {
            HasKey(e => e.Id);
            HasRequired(e => e.User).WithMany().HasForeignKey(e => e.UserId);
        }
    }
}
