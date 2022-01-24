using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
   public class PedidoViewModel
   {
      public int Codigo { get; set; }
      [Range(1,10)]      
      public virtual List<PizzaViewModel> Pizzas { get; set; }
      public DateTime Timestamp { get; set; }
      public double Total { get; set; }      
      public string CodigoCliente { get; set; }
      public virtual ClienteViewModel CodigoClienteNavigation { get; set; }
   }
}
