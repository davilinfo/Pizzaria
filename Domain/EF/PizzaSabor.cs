using Domain.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EF
{
   [Table("PizzaSabor")]
   public class PizzaSabor: EntidadeCodigo
   {
      public int CodigoPizza { get; set; }
      public int CodigoSabor { get; set; }

      [ForeignKey("CodigoPizza")]
      public virtual Pizza PizzaNavigation { get; set; }
      [ForeignKey("CodigoSabor")]
      public virtual Sabor SaborNavigation { get; set; }
   }
}
