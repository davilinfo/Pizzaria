using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Contracts
{
   public abstract class EntidadeCodigo
   {
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Key()]
      [Column("codigo")]
      public int Codigo { get; set; }
   }
}
