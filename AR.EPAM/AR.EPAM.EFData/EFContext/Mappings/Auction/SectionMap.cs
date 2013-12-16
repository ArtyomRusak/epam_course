using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.EFData.EFContext.Mappings.Auction
{
    public class SectionMap : EntityTypeConfiguration<Section>
    {
        public SectionMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Name).HasMaxLength(50).IsRequired();
            HasMany(e => e.Lots).WithRequired(e => e.Section).HasForeignKey(e => e.SectionId);
        }
    }
}