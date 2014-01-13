using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Bid : Entity<long>
    {
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Lot Lot { get; set; }
        public long LotId { get; set; }
    }
}
