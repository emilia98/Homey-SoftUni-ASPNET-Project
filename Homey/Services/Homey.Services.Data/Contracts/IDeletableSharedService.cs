using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homey.Services.Data.Contracts
{
    public interface IDeletableSharedService<TEntity> : ISharedService<TEntity>
    {
        IEnumerable<T> GetAll<T>(bool? withDeleted = false, int? count = null);

        T GetById<T>(int id, bool? withDeleted = false);

        Task Undelete(TEntity entity);
    }
}
