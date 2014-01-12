using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models.AuctionViewModels
{
    public class BidsViewModel : ViewModel
    {
        public HashSet<Bid> Bids { get; set; }
    }
}