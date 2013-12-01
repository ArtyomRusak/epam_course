using System.Collections.Generic;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership.ComplexTypes;

namespace AR.EPAM.Core.Entities.Membership
{
    public class User : Entity
    {
        public User()
        {
            Roles = new HashSet<Role>();
            Lots = new HashSet<Lot>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public ContactData ContactData { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Role> Roles { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
    }
}
