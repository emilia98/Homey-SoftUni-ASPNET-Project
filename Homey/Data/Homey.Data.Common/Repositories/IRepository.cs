using System;
using System.Linq;
using System.Threading.Tasks;

namespace Homey.Data.Common.Repositories
{
    public interface IRepository<TEntity>: IDisposable
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
