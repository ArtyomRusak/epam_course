using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Models
{
    public class LotViewModel : ViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double StartPrice { get; set; }
        public double CurrentPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public int DurationInDays { get; set; }
        public Currency Currency { get; set; }
        public User Owner { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<Bid> Bids { get; set; }
        [Range(0.01, Double.MaxValue)]
        public double BidValue { get; set; }
    }
}