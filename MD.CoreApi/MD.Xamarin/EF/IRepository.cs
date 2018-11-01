using System.Linq;
using System.Threading.Tasks;
using MD.Xamarin.EF.Models;

namespace MD.Xamarin.EF
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetAll(string userId);
        Task CreateAsync(TEntity item);
        Task<TEntity> GetByIdAsync(int? id);
        Task Update(TEntity item);
        Task<TEntity> DeleteAsync(int? id);
        Task SaveAsync();
    }
}