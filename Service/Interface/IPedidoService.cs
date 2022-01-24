using Application.Model;
using Application.Model.Request;
using System.Threading.Tasks;

namespace Application.Interface
{
   public interface IPedidoService : IService<PedidoViewModel>
   {
      public Task<PedidoViewModel> Adicionar(PedidoRequest entity);
      public Pagina<PedidoViewModel> Get(string codigoCliente, int page = 0, int pageSize = 20);
   }
}
