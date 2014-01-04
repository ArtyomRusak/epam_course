using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.EFData.EFContext.Mappings.Auction
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Name).HasMaxLength(50).IsRequired();
            HasMany(e => e.Lots).WithRequired(e => e.Category).HasForeignKey(e => e.SectionId);
        }
    }
}