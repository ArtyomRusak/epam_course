using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.Core.InterfaceRepositories;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.EFData.Repositories;

namespace AR.EPAM.EFData
{
    public class UnitOfWork : IRepositoryFactory, IUnitOfWork
    {
        private readonly AuctionContext _context;
        private IRepository<User, int> _userRepository;
        private IRepository<Role, int> _roleRepository;
        private bool _disposed;

        public UnitOfWork(AuctionContext context)
        {
            _context = context;
        }

        #region Implementation of IRepositoryFactory

        public IRepository<User, int> GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new Repository<User, int>(_context));
        }

        public IRepository<Role, int> GetRoleRepository()
        {
            return _roleRepository ?? (_roleRepository = new Repository<Role, int>(_context));
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        #endregion

        #region Implementation of IUnitOfWork

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void Rollback()
        {

        }

        #endregion
    }
}
