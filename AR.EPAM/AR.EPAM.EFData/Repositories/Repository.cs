using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.InterfaceRepositories;
using AR.EPAM.EFData.EFContext;

namespace AR.EPAM.EFData.Repositories
{
    public class Repository : IRepository
    {
        protected AuctionContext Context;

        public Repository(AuctionContext context)
        {
            Context = context;
        }
    }
}
