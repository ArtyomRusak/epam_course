using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.Core.Exceptions;
using AR.EPAM.Core.InterfaceRepositories;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.EFData.Repositories;

namespace AR.EPAM.EFData
{
    public class UnitOfWork : IRepositoryFactory, IUnitOfWork
    {
        #region [Private members]

        private readonly AuctionContext _context;
        private readonly DbContextTransaction _transaction;
        private IRepository<User, int> _userRepository;
        private IRepository<Role, int> _roleRepository;
        private IRepository<Profile, int> _profileRepository;
        private IRepository<Bid, long> _bidRepository;
        private IRepository<Comment, long> _commentRepository;
        private IRepository<Currency, int> _currencyRepository;
        private IRepository<Lot, long> _lotRepository;
        private IRepository<Section, int> _sectionRepository;
        private bool _disposed;
        private bool _isTransactionActive;

        #endregion


        #region [Ctor's]

        public UnitOfWork(AuctionContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
        }

        #endregion


        #region Implementation of IRepositoryFactory

        public IRepository<User, int> GetUserRepository()
        {
            return _userRepository ?? (_userRepository = new Repository<User, int>(_context));
        }

        public IRepository<Role, int> GetRoleRepository()
        {
            return _roleRepository ?? (_roleRepository = new Repository<Role, int>(_context));
        }

        public IRepository<Profile, int> GetProfileRepository()
        {
            return _profileRepository ?? (_profileRepository = new Repository<Profile, int>(_context));
        }

        public IRepository<Bid, long> GetBidRepository()
        {
            return _bidRepository ?? (_bidRepository = new Repository<Bid, long>(_context));
        }

        public IRepository<Comment, long> GetCommentRepository()
        {
            return _commentRepository ?? (_commentRepository = new Repository<Comment, long>(_context));
        }

        public IRepository<Currency, int> GetCurrencyRepository()
        {
            return _currencyRepository ?? (_currencyRepository = new Repository<Currency, int>(_context));
        }

        public IRepository<Lot, long> GetLotRepository()
        {
            return _lotRepository ?? (_lotRepository = new Repository<Lot, long>(_context));
        }

        public IRepository<Section, int> GetSectionRepository()
        {
            return _sectionRepository ?? (_sectionRepository = new Repository<Section, int>(_context));
        }

        #endregion


        #region Implementation of IDisposable

        public void Dispose()
        {
            if (_isTransactionActive)
            {
                try
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _isTransactionActive = false;
                }
                catch (Exception e)
                {
                    _transaction.Rollback();
                    _isTransactionActive = false;

                    _context.Dispose();
                    _disposed = true;

                    throw new RepositoryException(e);
                }
            }
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
                if (_isTransactionActive && !_disposed)
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _isTransactionActive = false;
                }
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                _isTransactionActive = false;
                throw new RepositoryException(e.Message);
            }
        }

        public void Rollback()
        {
            if (_isTransactionActive && !_disposed)
            {
                _transaction.Rollback();
                _isTransactionActive = false;
            }
        }

        public void PreSave()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
