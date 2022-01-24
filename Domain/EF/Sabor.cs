using Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EF
{
   [Table("Sabor")]
   public class Sabor : EntidadeCodigo
   {
      [Column("nome")]
      [MaxLength(50)]
      public string Nome { get; set; }
      [Column("valor")]
      public double Valor { get; set; }
      [Column("disponivel")]
      public bool Disponivel { get; set; }
   }
}
