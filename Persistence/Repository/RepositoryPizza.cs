using Domain.Contracts;
using Domain.EF;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
   public class RepositoryPizza : IRepositoryPizza
   {
      private Db _DB;

      public RepositoryPizza(Db context)
      {
         _DB = context;

      }

      public async Task<int> Add(Pizza entidade)
      {         
         _DB.Pizza.Add(entidade);

         return await _DB.SaveChangesAsync();
      }

      public IQueryable<Pizza> All()
      {
         return _DB.Pizza.AsNoTracking();
      }

      public async Task<int> Delete(int id)
      {
         var entidade = _DB.Pizza.Find(id);

         if (entidade != null)
         {
            _DB.Pizza.Remove(entidade);
         }

         return await _DB.SaveChangesAsync();
      }
      
      public async Task<Pizza> GetById(int id)
      {
         var entidade = await _DB.Pizza.FirstOrDefaultAsync(c => c.Codigo == id);

         return entidade;
      }

      public async Task<int> Update(Pizza entidade)
      {
         
          _DB.Pizza.Update(entidade);
         
         return await _DB.SaveChangesAsync();
      }
   }
}
