using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.EFData.EFContext.Mappings.Auction
{
    public class BidMap : EntityTypeConfiguration<Bid>
    {
        public BidMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Price).IsRequired();
            HasRequired(e => e.User).WithMany().HasForeignKey(e => e.UserId);
            HasRequired(e => e.Lot).WithMany().HasForeignKey(e => e.LotId);
            HasRequired(e => e.Currency).WithMany().HasForeignKey(e => e.CurrencyId);
        }
    }
}
