﻿using System;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Comment : Entity<long>
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Lot Lot { get; set; }
        public long LotId { get; set; }
    }
}
