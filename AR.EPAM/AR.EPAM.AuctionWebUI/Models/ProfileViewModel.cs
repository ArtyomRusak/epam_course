using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AR.EPAM.AuctionWebUI.Models
{
    public class ProfileViewModel : ViewModel
    {
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string UserName { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Email { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool Exist { get; set; }
    }
}