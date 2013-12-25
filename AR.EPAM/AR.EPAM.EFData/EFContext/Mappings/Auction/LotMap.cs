using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.EFData.EFContext.Mappings.Auction
{
    public class LotMap : EntityTypeConfiguration<Lot>
    {
        public LotMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Name).HasMaxLength(50).IsRequired();
            Property(e => e.StartPrice).IsRequired();
            Property(e => e.CurrentPrice).IsRequired();
            Property(e => e.CreateDate).IsRequired();
            Property(e => e.Duration).IsRequired();
            HasRequired(e => e.Currency).WithMany().HasForeignKey(e => e.CurrencyId);
            HasRequired(e => e.Owner).WithMany(e => e.Lots).HasForeignKey(e => e.OwnerId).WillCascadeOnDelete(false);
            HasRequired(e => e.Section).WithMany(e => e.Lots).HasForeignKey(e => e.SectionId);
            HasMany(e => e.Bids).WithRequired(e => e.Lot).HasForeignKey(e => e.LotId);
        }
    }
}
