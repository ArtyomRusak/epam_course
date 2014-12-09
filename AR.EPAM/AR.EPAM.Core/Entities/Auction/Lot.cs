using System;
using System.Collections.Generic;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Lot : Entity<long>
    {
        public Lot()
        {
            Bids = new HashSet<Bid>();
        }

        public string Name { get; set; }
        public double StartPrice { get; set; }
        public double CurrentPrice { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int DurationInDays { get; set; }
        public virtual Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        public virtual User Owner { get; set; }
        public int OwnerId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public string PathToImage { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
