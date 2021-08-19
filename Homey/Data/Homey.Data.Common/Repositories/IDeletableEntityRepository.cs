using System;
using System.Linq;
using Homey.Data.Common.Models;

namespace Homey.Data.Common.Repositories
{
    public interface IDeletableEntityRepository<TEntity>: IRepository<TEntity>
        where TEntity: class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
