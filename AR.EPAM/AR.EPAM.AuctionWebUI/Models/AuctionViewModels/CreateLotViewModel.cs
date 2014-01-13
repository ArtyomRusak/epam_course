using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models.AuctionViewModels
{
    public class CreateLotViewModel : ViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.01, Double.MaxValue)]
        public double StartPrice { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, 90)]
        public int DurationInDays { get; set; }
        [Required]
        public string SelectedCurrency { get; set; }
        public HashSet<string> Currencies { get; set; }
        public CategoriesViewModel CategoriesViewModel { get; set; }
    }
}