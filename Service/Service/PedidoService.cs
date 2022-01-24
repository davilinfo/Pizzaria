using Application.Exception;
using Application.Interface;
using Application.Model;
using Application.Model.Request;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Contracts;
using AutoMapper;
using Domain.EF;

namespace Application.Service
{
   public class PedidoService : IPedidoService
   {
      private IRepositorySabor _repositorySabor;
      private IRepositoryCliente _repositoryCliente;
      private IRepositoryPedido _repositoryPedido;
      private IRepositoryPizza _repositoryPizza;
      private IMapper _mapper;
      private readonly string _NaoIdentificadoException = "Não foi informado codido ou endereço do cliente! Uma dessas informações é obrigatória";
      private readonly string _PedidoSemPizza = "Nenhuma pizza foi solicitada no pedido! Você deve pedir entre 1 e 10 pizzas";
      private readonly string _PedidoMuitaPizza = "Máximo número de pizzas é 10 em um único pedido";
      private readonly string _SaborPizzaNaoDisponivel = $"Você pediu alguma pizza de um sabor que não temos no momento. Sabores indisponíveis";
      private readonly string _ClienteNaoEncontrado = $"Cliente de codigo {0} não foi encontrado";
      private readonly string _ErroAoRegistrarCliente = "Ocorreu erro ao registrar novo cliente";
      private readonly string _PizzaSemSabor = "Pizza deve ter um sabor";
      private readonly string _PizzaMuitoSabor = "Pizza deve ter no máximo 2 sabores";
      
      public PedidoService(IMapper mapper, IRepositorySabor repositorySabor, IRepositoryCliente repositoryCliente, 
         IRepositoryPedido repositoryPedido, IRepositoryPizza repositoryPizza)
      {
         _mapper = mapper;
         _repositorySabor = repositorySabor;
         _repositoryCliente = repositoryCliente;
         _repositoryPedido = repositoryPedido;
         _repositoryPizza = repositoryPizza;
      }
      
      public Task<PedidoViewModel> Adicionar(PedidoViewModel entity)
      {
         throw new NotImplementedException();
      }

      public async Task<PedidoViewModel> Adicionar(PedidoRequest entity)
      {
         Valida(entity);
         
         if (!string.IsNullOrEmpty(entity.CodigoCliente))
         {
            var cliente = await _repositoryCliente.GetById(entity.CodigoCliente);
            if (cliente == null) {
               throw new BusinessException(string.Format(_ClienteNaoEncontrado, entity.CodigoCliente));
            }            
         }
         else
         {
            var cliente = new Cliente();
            cliente.Endereco = entity.Endereco;
            cliente.Codigo = cliente.GenerateClienteHash();
            if (!(await _repositoryCliente.Add(cliente) > 0))
            {
               throw new BusinessException(_ErroAoRegistrarCliente);
            }
            entity.CodigoCliente = cliente.Codigo;
         }

         var pedido = new Pedido
         {
            CodigoCliente = entity.CodigoCliente,
            Pizzas = await GetPizzas(entity.Pizzas),
            Timestamp = DateTime.Now
         };

         pedido.Total = await GetTotalPedido(entity.Pizzas);         

         if (await _repositoryPedido.Add(pedido) > 0)
         {
            return new PedidoViewModel { 
               Codigo = pedido.Codigo, 
               CodigoCliente = pedido.CodigoCliente, 
               CodigoClienteNavigation = _mapper.Map<ClienteViewModel>(pedido.CodigoClienteNavigation),
               Pizzas = (from p in pedido.Pizzas 
                        select new PizzaViewModel { 
                           Codigo = p.Codigo, 
                           Sabores = (from s in p.Sabores 
                                     select new SaborViewModel { 
                                        Codigo = s.Codigo, 
                                        Nome = s.SaborNavigation.Nome, 
                                        Disponivel = s.SaborNavigation.Disponivel, 
                                        Valor = s.SaborNavigation.Valor 
                                     }).ToList()
                        }).ToList(),
               Timestamp = pedido.Timestamp,
               Total = pedido.Total
            };
         }
         else
         {
            return null;
         }
      }

      public Task<PedidoViewModel> Atualizar(PedidoViewModel entity)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<PedidoViewModel> Get()
      {
         throw new NotImplementedException();
      }

      public Pagina<PedidoViewModel> Get(string codigoCliente, int page = 0, int pageSize = 20)
      {
         var pedidosCliente = from p in _repositoryPedido.All().Where(p => p.CodigoCliente == codigoCliente)
                              orderby p.Codigo descending
                              select new PedidoViewModel
                              {
                                 Codigo = p.Codigo,
                                 CodigoCliente = p.CodigoCliente,
                                 CodigoClienteNavigation = _mapper.Map<ClienteViewModel>(p.CodigoClienteNavigation),
                                 Pizzas = (from pizza in p.Pizzas
                                           select new PizzaViewModel
                                           {
                                              Codigo = pizza.Codigo,                                             
                                              Sabores = (from s in pizza.Sabores
                                                         select new SaborViewModel
                                                         {
                                                            Codigo = s.Codigo,
                                                            Nome = s.SaborNavigation.Nome,
                                                            Disponivel = s.SaborNavigation.Disponivel,
                                                            Valor = s.SaborNavigation.Valor
                                                         }).ToList()
                                           }).ToList(),
                                 Total = p.Total,
                                 Timestamp = p.Timestamp
                              }                              
                              ;

         return new Pagina<PedidoViewModel>(pedidosCliente, pageSize, page, pedidosCliente.Count());
      }

      public Task<PedidoViewModel> GetById(int id)
      {
         throw new NotImplementedException();
      }

      public Task<bool> Remover(int id)
      {
         throw new NotImplementedException();
      }

      private void Valida(PedidoRequest entity)
      {
         if (string.IsNullOrEmpty(entity.CodigoCliente) && string.IsNullOrEmpty(entity.Endereco))
         {
            throw new BusinessException(_NaoIdentificadoException);
         }

         if (!entity.Pizzas.Any())
         {
            throw new BusinessException(_PedidoSemPizza);
         }

         if (entity.Pizzas.Count > 10)
         {
            throw new BusinessException(_PedidoMuitaPizza);
         }

         var saboresIndisponiveis = from s in _repositorySabor.All().Where(s => s.Disponivel == false)
                                    select _mapper.Map<SaborViewModel>(s);

         foreach (var pizza in entity.Pizzas)
         {
            if (pizza.Sabores.Select(p=>p.Codigo).Intersect(saboresIndisponiveis.Select(s=>s.Codigo)).Count() > 0)
            {
               var nomeSaboresIndisponiveis = new System.Text.StringBuilder();
               foreach (var sabor in saboresIndisponiveis)
               {
                  nomeSaboresIndisponiveis.AppendLine(sabor.Nome);
               }
               throw new BusinessException($"{_SaborPizzaNaoDisponivel} {nomeSaboresIndisponiveis.ToString()}");
            }

            if (!pizza.Sabores.Any())
            {
               throw new BusinessException($"{_PizzaSemSabor}");
            }

            if (pizza.Sabores.Count > 2)
            {
               throw new BusinessException($"{_PizzaMuitoSabor}");
            }
         }
      }

      private async Task<double> GetTotalPedido(IEnumerable<PizzaViewModel> pizzas)
      {
         double total = 0;
         foreach(var pizza in pizzas)
         {
            double totalPizza = 0;
            foreach(var sabor in pizza.Sabores)
            {
               totalPizza += (await _repositorySabor.GetById(sabor.Codigo)).Valor;
            }
            
            total += (totalPizza / pizza.Sabores.Count);
         }

         return total;
      }

      private async Task<List<Pizza>> GetPizzas(IEnumerable<PizzaViewModel> pizzasModel)
      {
         var pizzas = new List<Pizza>();
         foreach(var pizzaModel in pizzasModel)
         {
            double totalPizza = 0;
            foreach (var sabor in pizzaModel.Sabores)
            {
               totalPizza += (await _repositorySabor.GetById(sabor.Codigo)).Valor;
            }
            var pizza = new Pizza { 
               Codigo = pizzaModel.Codigo,
               Sabores = GetSabores(pizzaModel.Sabores),
               Valor = totalPizza / pizzaModel.Sabores.Count
            };
            pizzas.Add(pizza);
         }

         return pizzas;
      }

      private List<PizzaSabor> GetSabores(IEnumerable<SaborViewModel> saboresModel)
      {
         var sabores = new List<PizzaSabor>();
         foreach(var saborModel in saboresModel)
         {            
            sabores.Add(new PizzaSabor { CodigoSabor = saborModel.Codigo, PizzaNavigation = new Pizza() });
         }

         return sabores;
      }
   }
}
