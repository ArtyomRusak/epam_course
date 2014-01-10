using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.AuctionWebUI.Models.MembershipViewModels;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Models.AdministrationViewModels
{
    public class AdminUserViewModel : ViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public virtual HashSet<Role> Roles { get; set; }
        public virtual HashSet<Lot> Lots { get; set; }
    }
}