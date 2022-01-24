using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
   public interface IService<Model> where Model : class
   {
      public Task<Model> GetById(int id);

      public IEnumerable<Model> Get();

      public Task<Model> Adicionar(Model entity);

      public Task<bool> Remover(int id);

      public Task<Model> Atualizar(Model entity);
   }
}
