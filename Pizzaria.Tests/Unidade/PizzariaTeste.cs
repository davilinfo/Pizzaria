using Application.Interface;
using Application.Model;
using Application.Service;
using AutoMapper;
using Domain.Contracts;
using Domain.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Pizzaria.Tests.Unidade
{
   [TestClass]
   public class PizzariaTeste
   {
      [TestMethod]
      public void DeveRetornarPedido()
      {         
         var repositoryPedido = new Mock<IRepositoryPedido>();
         var repositorySabor = new Mock<IRepositorySabor>();
         var repositoryPizza = new Mock<IRepositoryPizza>();
         var repositoryCliente = new Mock<IRepositoryCliente>();
         var mapper = new Mock<IMapper>();

         var pedidoViewModel = new PedidoViewModel()
         {
            Codigo = 1,
            CodigoCliente = "AABD7242D70A8322E33B50316EB638D3F0055EE6A1C8A4FBF37778B8AC5EB863",
            CodigoClienteNavigation = new ClienteViewModel
            {
               Codigo = "AABD7242D70A8322E33B50316EB638D3F0055EE6A1C8A4FBF37778B8AC5EB863",
               Endereco = "endereço2",
               Nome = "cliente2",
               Telefone = "22222-2222",
               Timestamp = new System.DateTime(2022, 1, 23, 22, 30, 20, 716)
            },
            Timestamp = new System.DateTime(2022, 1, 23, 23, 41, 04, 823),
            Total = 42.5
         };

         var listPizza = new List<Pizza>();
         listPizza.Add(new Pizza
         {
            Codigo = 1,
            Valor = 42.5,
            Sabores = new List<PizzaSabor>
            {
               new PizzaSabor{
                  Codigo = 1,
                  CodigoPizza = 1,
                  CodigoSabor = 1,
                  SaborNavigation = new Sabor
                  {
                     Codigo = 1,
                     Nome = "Mussarela",
                     Disponivel = true,
                     Valor = 42.5
                  }
               }
            }
         });

         var listPedido = new List<Pedido>();
         listPedido.Add(new Pedido
         {
            Codigo = 1,
            CodigoCliente = "AABD7242D70A8322E33B50316EB638D3F0055EE6A1C8A4FBF37778B8AC5EB863",
            CodigoClienteNavigation = new Cliente
            {
               Codigo = "AABD7242D70A8322E33B50316EB638D3F0055EE6A1C8A4FBF37778B8AC5EB863",
               Endereco = "endereço2",
               Nome = "cliente2",
               Telefone = "22222-2222",
               Timestamp = new System.DateTime(2022, 1, 23, 22, 30, 20, 716)
            },
            Pizzas = listPizza,
            Timestamp = new System.DateTime(2022, 1, 23, 23, 41, 04, 823),
            Total = 42.5
         });
         
         var setup = repositoryPedido.Setup(p => p.All()).Returns(listPedido.AsQueryable<Pedido>());

         var service = new PedidoService(mapper.Object, repositorySabor.Object, repositoryCliente.Object, repositoryPedido.Object, repositoryPizza.Object);

         var pagina = service.Get("AABD7242D70A8322E33B50316EB638D3F0055EE6A1C8A4FBF37778B8AC5EB863");
         repositoryPedido.VerifyAll();

         Assert.IsNotNull(pagina);
      }      
   }
}
