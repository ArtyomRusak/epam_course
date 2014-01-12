using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AR.EPAM.AuctionWebUI.Models.AuctionViewModels;
using AR.EPAM.Core.Entities.Auction;
using Microsoft.Ajax.Utilities;

namespace AR.EPAM.AuctionWebUI.Models.AdministrationViewModels
{
    public class SearchLotViewModel : ViewModel
    {
        public string Name { get; set; }
        public double MinBorder { get; set; }
        public double MaxBorder { get; set; }
        public string SelectedCurrency { get; set; }
        public HashSet<string> Currencies { get; set; }
        public CategoriesViewModel CategoriesViewModel { get; set; }
        public LotsPartialViewModel LotsPartialViewModel { get; set; }
    }
}