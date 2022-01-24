using Domain.Contracts;
using Domain.EF;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
   public class RepositoryPedido : IRepositoryPedido
   {
      private Db _DB;

      public RepositoryPedido(Db context)
      {
         _DB = context;

      }

      public async Task<int> Add(Pedido entidade)
      {
         entidade.Timestamp = System.DateTime.Now;
         _DB.Pedido.Add(entidade);

         return await _DB.SaveChangesAsync();
      }

      public IQueryable<Pedido> All()
      {
         return _DB.Pedido.Include(p=> p.CodigoClienteNavigation).AsNoTracking();
      }

      public async Task<int> Delete(int id)
      {
         var entidade = _DB.Pedido.Find(id);

         if (entidade != null)
         {
            _DB.Pedido.Remove(entidade);
         }

         return await _DB.SaveChangesAsync();
      }
      
      public async Task<Pedido> GetById(int id)
      {
         var entidade = await _DB.Pedido.Include(p => p.CodigoClienteNavigation).FirstOrDefaultAsync(c => c.Codigo == id);

         return entidade;
      }

      public async Task<int> Update(Pedido entidade)
      {
         
          _DB.Pedido.Update(entidade);
         
         return await _DB.SaveChangesAsync();
      }
   }
}
