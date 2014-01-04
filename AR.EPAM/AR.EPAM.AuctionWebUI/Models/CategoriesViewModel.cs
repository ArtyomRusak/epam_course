using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models
{
    public class CategoriesViewModel : ViewModel
    {
        public int? Seed { get; set; }
        public ICollection<Category> Categories { get; set; } 
    }
}