using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Core.Entities
{
    public class EntityWithVersion<TKey> : Entity<TKey>, IVersion
    {
        public virtual byte[] Version { get; set; }

    }
}
