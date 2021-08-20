using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homey.Services.Data.Contracts
{
    public interface ISharedService<TEntity>
    {
        Task Create(TEntity entity);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}
