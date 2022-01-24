using Domain.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.EF
{
   [Table("Pizza")]
   public class Pizza : EntidadeCodigo
   {            
      [Column("valor")]
      public double Valor { get; set; }      
      public virtual List<PizzaSabor> Sabores { get; set; }
   }
}
