using System;
using System.Collections.Generic;

namespace Application.Model.Request
{
   public class PedidoRequest
   {
      public virtual IList<PizzaViewModel> Pizzas { get; set; }
      public string CodigoCliente { get; set; }
      public string Endereco { get; set; }
      public DateTime Timestamp { get; set; }
   }
}
