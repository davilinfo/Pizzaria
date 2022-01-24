using Application.Model;
using Application.Model.Request;
using AutoMapper;
using Domain.EF;

namespace Application.AutoMapper
{
   public class ViewModelToDomainMapping : Profile
   {
      public ViewModelToDomainMapping()
      {
         CreateMap<PedidoRequest, Pedido>();
         CreateMap<PizzaViewModel, Pizza>();
         CreateMap<SaborViewModel, Sabor>();
         CreateMap<ClienteViewModel, Cliente>();
         CreateMap<PizzaSaborViewModel, PizzaSabor>();
      }      
   }
}
