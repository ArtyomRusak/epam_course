using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AR.EPAM.AuctionWebUI.Models.AuctionViewModels;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Models.AdministrationViewModels
{
    public class AdminLotViewModel : ViewModel
    {
        public long LotId { get; set; }
        [Required]
        public string Name { get; set; }
        public double StartPrice { get; set; }
        public double CurrentPrice { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int DurationInDays { get; set; }
        public Currency Currency { get; set; }
        public User Owner { get; set; }
        public Category Category { get; set; }
        public CategoriesViewModel CategoriesViewModel { get; set; }
        public BidsViewModel BidsViewModel { get; set; }
    }
}