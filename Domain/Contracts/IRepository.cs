using System.Linq;
using System.Threading.Tasks;

namespace Domain.Contracts
{
   public interface IRepository<Entity>
   {
      public Task<int> Add(Entity entidade);
      public Task<int> Update(Entity entidade);
      public Task<int> Delete(int id);
      public IQueryable<Entity> All();
      public Task<Entity> GetById(int id);
   }
}
