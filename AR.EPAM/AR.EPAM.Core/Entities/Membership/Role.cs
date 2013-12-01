using System.Collections.Generic;

namespace AR.EPAM.Core.Entities.Membership
{
    public class Role : Entity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } 
    }
}
