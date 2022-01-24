using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
   public class Pagina<T>
   {
      public int PaginaTamanho { get; private set; } = 0;

      public int PaginaNumero { get; private set; } = 0;

      public int PaginaTotal { get; private set; } = 0;

      public int TotalItens { get; private set; } = 0;

      public IEnumerable<T> Itens { get; private set; }

      public Pagina()
      {
         Itens = new List<T>();
         PaginaTamanho = 0;
         PaginaNumero = 0;
         PaginaTotal = 0;
         TotalItens = 0;
      }

      public Pagina(IEnumerable<T> itens, int paginaTamanho, int paginaNumero, int totalItens)
      {
         Itens = itens.ToList();
         PaginaTamanho = paginaTamanho;
         PaginaNumero = paginaNumero;
         PaginaTotal = paginaTamanho > 0 ? (int)Math.Round((decimal)totalItens / paginaTamanho) : 0;
         TotalItens = totalItens;
      }
   }
}
