using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Models.AdministrationViewModels
{
    public class SearchUserViewModel : ViewModel
    {
        public SearchUserViewModel()
        {
            UsersPartialViewModel = new UsersPartialViewModel();
        }

        public HashSet<string> Criteria { get; set; }
        [Required]
        public string SelectedCriterion { get; set; }
        public string FindString { get; set; }
        public UsersPartialViewModel UsersPartialViewModel { get; set; }
    }
}