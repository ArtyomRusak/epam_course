using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Core.Entities.Membership
{
    public class Profile : Entity<int>
    {
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
