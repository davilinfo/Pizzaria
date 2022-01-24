using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EF
{
   public class Pedido : EntidadeCodigo
   {  
      [Column("CodigoCliente")]
      public string CodigoCliente { get; set; }      
      [Column("Timestamp")]
      public DateTime Timestamp { get; set; }
      [Column("Total")]
      public double Total { get; set; }
      [ForeignKey("CodigoCliente")]
      public virtual Cliente CodigoClienteNavigation { get; set; }      
      public virtual IList<Pizza> Pizzas { get; set; }
   }
}
