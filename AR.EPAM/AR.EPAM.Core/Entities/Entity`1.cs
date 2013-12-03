using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Core.Entities
{
    public class Entity<T> : Entity
    {
        public T Id { get; set; }
    }
}
