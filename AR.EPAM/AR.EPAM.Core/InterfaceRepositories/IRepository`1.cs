using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities;

namespace AR.EPAM.Core.InterfaceRepositories
{
    public interface IRepository<TEntity> : IRepository where TEntity : Entity
    {
        void Create(TEntity value);
        void Update(TEntity value);
        void Remove(TEntity value);
        TEntity GetEntityById(int id);
        TEntity Find(Func<TEntity, bool> predicate);
        IQueryable<TEntity> All();
        IQueryable<TEntity> Filter(Func<TEntity, bool> predicate);
        void Save();
    }
}
