using System.Collections.Generic;

namespace AR.EPAM.Core.Entities.Auction
{
    public class Category : Entity<int>
    {
        public Category()
        {
            Lots = new HashSet<Lot>();
        }

        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
    }
}