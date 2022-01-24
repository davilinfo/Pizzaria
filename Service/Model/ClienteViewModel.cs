using System;

namespace Application.Model
{
   public class ClienteViewModel : IFormattable
   {
      private string _codigo = "codigo";
      private string _nome = "nome";
      private string _endereco = "endereço";
      public string Codigo { get; set; }
      public string Nome { get; set; }
      public DateTime Timestamp { get; set; }
      public string Endereco { get; set; }
      public string Telefone { get; set; }
      public string ToString(string format, IFormatProvider formatProvider)
      {
         return $"{_codigo}:{Codigo}, {_nome}: {Nome}, {_endereco}: {Endereco}";
      }
   }
}
