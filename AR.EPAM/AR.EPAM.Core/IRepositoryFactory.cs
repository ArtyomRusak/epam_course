using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.Core.InterfaceRepositories;

namespace AR.EPAM.Core
{
    public interface IRepositoryFactory
    {   
        IRepository<User, int> GetUserRepository();
        IRepository<Role, int> GetRoleRepository();
        IRepository<Profile, int> GetProfileRepository();
        IRepository<Bid, long> GetBidRepository();

        IRepository<Comment, long> GetCommentRepository();
        IRepository<Currency, int> GetCurrencyRepository();
        IRepository<Lot, long> GetLotRepository();
        IRepository<Category, int> GetSectionRepository();
    }
}
