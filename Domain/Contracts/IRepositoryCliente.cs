using Domain.EF;
using System.Threading.Tasks;

namespace Domain.Contracts
{
   public interface IRepositoryCliente : IRepository<Cliente>
   {
      public Task<Cliente> GetById(string codigo);
   }
}
