using Domain.Contracts;
using Domain.EF;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
   public class RepositoryCliente : IRepositoryCliente
   {
      private Db _DB;

      public RepositoryCliente(Db context)
      {
         _DB = context;

      }

      public async Task<int> Add(Cliente entidade)
      {
         entidade.Timestamp = DateTime.Now;
         _DB.Cliente.Add(entidade);

         return await _DB.SaveChangesAsync();
      }

      public IQueryable<Cliente> All()
      {
         return _DB.Cliente.AsNoTracking();
      }

      public async Task<int> Delete(int id)
      {
         var entidade = _DB.Cliente.Find(id);

         if (entidade != null)
         {
            _DB.Cliente.Remove(entidade);            
         }

         return await _DB.SaveChangesAsync();
      }

      public async Task<Cliente> GetById(string codigo)
      {
         var entidade = await _DB.Cliente.FirstOrDefaultAsync(c=> c.Codigo == codigo);

         return entidade;
      }

      public Task<Cliente> GetById(int id)
      {
         throw new NotImplementedException();
      }

      public async Task<int> Update(Cliente entidade)
      {
         
          _DB.Cliente.Update(entidade);
         
         return await _DB.SaveChangesAsync();
      }
   }
}
