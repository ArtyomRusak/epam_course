using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Models
{
    public class CommentViewModel : ViewModel
    {
        public HashSet<Comment> Comments { get; set; }
        [Required]
        public string Description { get; set; }
        public string UserMail { get; set; }
        public long LotId { get; set; }
    }
}