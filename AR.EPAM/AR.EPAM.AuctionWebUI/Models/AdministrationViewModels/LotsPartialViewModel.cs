using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models.AdministrationViewModels
{
    public class LotsPartialViewModel : ViewModel
    {
        public HashSet<Lot> Lots { get; set; }
    }
}