using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Models.AdministrationViewModels
{
    public class UsersPartialViewModel : ViewModel
    {
        public HashSet<User> Users { get; set; }
    }
}