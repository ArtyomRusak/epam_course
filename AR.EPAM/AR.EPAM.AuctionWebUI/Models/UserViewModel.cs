using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Models
{
    public class UserViewModel : ViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public HashSet<Role> Roles { get; set; }
        public HashSet<Lot> Lots { get; set; }
    }
}