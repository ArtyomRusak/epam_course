using System.Collections.Generic;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Models.MembershipViewModels
{
    public class UserViewModel : ViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public HashSet<Role> Roles { get; set; }
        public HashSet<Lot> Lots { get; set; }
    }
}