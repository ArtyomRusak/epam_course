using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models
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
        [Range(0, 90)]
        public int DurationInDays { get; set; }
        [Required]
        public string SelectedCurrency { get; set; }
        [Required]
        public string SelectedCategory { get; set; }
        public HashSet<string> Currencies { get; set; }
        public HashSet<Category> Categories { get; set; }
    }
}