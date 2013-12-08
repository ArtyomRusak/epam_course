using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Section : Entity<int>
    {
        public string Name { get; set; }
        public int? SectionId { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
    }
}
