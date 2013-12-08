using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Currency : Entity<int>
    {
        public string Value { get; set; }
    }
}
