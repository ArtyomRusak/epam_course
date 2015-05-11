using System.Collections.Generic;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models.AuctionViewModels
{
    public class BidsViewModel : ViewModel
    {
        public HashSet<Bid> Bids { get; set; }
    }
}