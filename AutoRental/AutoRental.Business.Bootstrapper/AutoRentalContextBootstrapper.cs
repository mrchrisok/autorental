using AutoRental.Data;

namespace AutoRental.Business.Bootstrapper
{
   /// <summary>
   /// Used to execute migrations and initialize the AutoRentalContext
   /// </summary>
   public static class AutoRentalContextBootstrapper
   {
      public static void Init()
      {
         AutoRentalContextInitializer.Init();
      }
   }
}
