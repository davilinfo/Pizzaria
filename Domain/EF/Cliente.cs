using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EF
{
   [Table("Cliente")]
   public class Cliente
   {            
      [Key()]
      [Column("codigo")]
      public string Codigo { get; set; }
      [Column("nome")]
      [MaxLength(90)]
      public string Nome { get; set; }
      [Column("Timestamp")]
      public DateTime Timestamp { get; set; }
      [Column("endereco")]
      [MaxLength(2000)]
      public string Endereco { get; set; }
      [Column("telefone")]
      [MaxLength(15)]
      public string Telefone { get; set; }      

      public string GenerateClienteHash()
      {        
         var sha256 = System.Security.Cryptography.SHA256.Create();
         
         sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Nome ?? "" + Endereco + Telefone ?? ""));
         var result = System.Convert.ToHexString(sha256.Hash);
         return result;
      }
   }
}
