using System;
using System.Collections.Generic;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.Core.Entities.Membership
{
    public class User : Entity<int>
    {
        public User()
        {
            Roles = new HashSet<Role>();
            Lots = new HashSet<Lot>();
        }

        public string UserName { get; set; }
        public int Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public ICollection<Role> Roles { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public bool IsLogged { get; set; }

        public void SetPassword(string password)
        {
            var passwordHash = (password + this.PasswordSalt).GetHashCode();
            Password = passwordHash;
        }
    }
}
