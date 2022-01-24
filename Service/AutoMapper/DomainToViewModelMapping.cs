using Application.Model;
using AutoMapper;
using Domain.EF;

namespace Application.AutoMapper
{
   public class DomainToViewModelMapping : Profile
   {
      public DomainToViewModelMapping()
      {
         CreateMap<Sabor, SaborViewModel>();
         CreateMap<Pizza, PizzaViewModel>();
         CreateMap<Cliente, ClienteViewModel>();
         CreateMap<Pedido, PedidoViewModel>();
         CreateMap<PizzaSabor, PizzaSaborViewModel>();
      }
   }
}
