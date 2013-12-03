using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Comment : Entity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Lot Lot { get; set; }
        public int LotId { get; set; }
    }
}
