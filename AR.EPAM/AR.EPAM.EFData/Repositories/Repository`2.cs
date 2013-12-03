using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities;
using AR.EPAM.Core.InterfaceRepositories;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Infrastructure.Guard;

namespace AR.EPAM.EFData.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity
    {
        #region [Private members]

        private AuctionContext _context;
        private DbSet<TEntity> _entities;

        #endregion


        #region [Ctor's]

        public Repository(AuctionContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public Repository()
        {
            _context = new AuctionContext();
            _entities = _context.Set<TEntity>();
        }

        #endregion


        #region Implementation of IRepository<TEntity>

        public void Create(TEntity value)
        {
            Guard.AgainstNullReference(value, "value");

            _entities.Add(value);
        }

        public void Update(TEntity value)
        {
            Guard.AgainstNullReference(value, "value");

            _entities.Attach(value);
            _context.Entry(value).State = EntityState.Modified;
        }

        public void Remove(TEntity value)
        {
            Guard.AgainstNullReference(value, "value");

            _entities.Remove(value);
        }

        public TEntity GetEntityById(TKey id)
        {
            Guard.AgainstNullReference(id, "id");

            try
            {
                return _entities.Find(id);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            Guard.AgainstNullReference(predicate, "predicate");

            return _entities.Where(predicate).SingleOrDefault();
        }

        public IQueryable<TEntity> All()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
