using Domain.Contracts;
using Domain.EF;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
   public class RepositorySabor : IRepositorySabor
   {
      private Db _DB;

      public RepositorySabor(Db context)
      {
         _DB = context;

      }

      public async Task<int> Add(Sabor entidade)
      {         
         _DB.Sabores.Add(entidade);

         return await _DB.SaveChangesAsync();
      }

      public IQueryable<Sabor> All()
      {
         return _DB.Sabores.AsNoTracking();
      }

      public async Task<int> Delete(int id)
      {
         var entidade = _DB.Sabores.Find(id);

         if (entidade != null)
         {
            entidade.Disponivel = false;
            _DB.Sabores.Update(entidade);
         }

         return await _DB.SaveChangesAsync();
      }
      
      public async Task<Sabor> GetById(int id)
      {
         var entidade = await _DB.Sabores.FirstOrDefaultAsync(c => c.Codigo == id);

         return entidade;
      }

      public async Task<int> Update(Sabor entidade)
      {         
          _DB.Sabores.Update(entidade);
         
         return await _DB.SaveChangesAsync();
      }
   }
}
