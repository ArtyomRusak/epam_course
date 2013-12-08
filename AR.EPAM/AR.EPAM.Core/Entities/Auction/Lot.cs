using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Lot : Entity<long>
    {
        public string Name { get; set; }
        public double StartPrice { get; set; }
        public double CurrentPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan Duration { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public virtual Section Section { get; set; }
        public int SectionId { get; set; }
        public ICollection<Bid> Bids { get; set; }
    }
}
