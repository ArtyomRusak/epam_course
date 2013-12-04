using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.Core.InterfaceRepositories;

namespace AR.EPAM.Core
{
    public interface IRepositoryFactory
    {
        IRepository<User, int> GetUserRepository();
        IRepository<Role, int> GetRoleRepository();
    }
}
