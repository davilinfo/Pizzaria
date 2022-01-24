namespace Application.Model
{
   public class PizzaSaborViewModel
   {
      public int CodigoPizza { get; set; }
      public int CodigoSabor { get; set; }

      
      public virtual PizzaViewModel PizzaNavigation { get; set; }
      
      public virtual SaborViewModel SaborNavigation { get; set; }
   }
}
