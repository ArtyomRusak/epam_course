using System.Collections.Generic;

namespace AR.EPAM.Core.Entities.Membership.ComplexTypes
{
    public class ContactData
    {
        public string City { get; set; }
        public ICollection<string> PhoneNumber { get; set; }
    }
}
