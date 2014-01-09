using System.Collections.Generic;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Models.MembershipViewModels
{
    public class ProfileViewModel : ViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public HashSet<Lot> Lots { get; set; }
        public HashSet<Lot> BiddedLots { get; set; }
    }
}