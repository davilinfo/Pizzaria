using System.Collections.Generic;

namespace Application.Model
{
   public class PizzaViewModel
   {
      public int Codigo { get; set; }
      public List<SaborViewModel> Sabores { get; set; }
   }
}
