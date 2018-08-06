using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.CoreApi.Models
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        Task CreateAsync(TEntity item);
        Task<TEntity> GetByIdAsync(int? id);
        void Update(TEntity item);
        Task<TEntity> DeleteAsync(int? id);
        Task SaveAsync();
    }
}