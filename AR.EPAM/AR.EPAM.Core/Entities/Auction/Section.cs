using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Section : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
    }
}
