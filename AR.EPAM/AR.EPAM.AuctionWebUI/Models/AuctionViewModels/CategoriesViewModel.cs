using System.Collections.Generic;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models.AuctionViewModels
{
    public class CategoriesViewModel : ViewModel
    {
        public int? Seed { get; set; }
        public ICollection<Category> Categories { get; set; } 
    }
}