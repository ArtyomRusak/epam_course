using System.Collections.Generic;

namespace AR.EPAM.Core.Entities.Membership
{
    public class Role : Entity<int>
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public ICollection<User> Users { get; set; } 
    }
}
