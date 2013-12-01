using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.EFData.EFContext.Mappings.Auction
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Description).HasMaxLength(1000).IsRequired();
            Property(e => e.Date).IsRequired();
            //HasRequired(e => e.Lot).WithMany().HasForeignKey(e => e.LotId);
            HasRequired(e => e.User).WithMany().HasForeignKey(e => e.UserId);
        }
    }
}
