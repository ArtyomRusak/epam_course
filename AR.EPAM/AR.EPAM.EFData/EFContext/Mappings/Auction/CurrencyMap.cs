using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.EFData.EFContext.Mappings.Auction
{
    public class CurrencyMap : EntityTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Value).IsRequired();
        }
    }
}
