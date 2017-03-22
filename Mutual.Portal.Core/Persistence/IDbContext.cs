
using System.Threading.Tasks;
using System.Data.Entity;

namespace Mutual.Portal.Core.Persistence
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int Save();
        Task<int> SaveAsync();
    }
}
