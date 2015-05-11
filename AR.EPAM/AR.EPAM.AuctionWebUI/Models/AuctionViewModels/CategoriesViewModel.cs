using System.Collections.Generic;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models.AuctionViewModels
{
    public class CategoriesViewModel : ViewModel
    {
        public string SelectedCategory { get; set; }
        public HashSet<Category> Categories { get; set; }
    }
}