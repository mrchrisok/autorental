using System;
using System.Data.Entity.Migrations;
using System.IO;

namespace AutoRental.Data
{
   /// <summary>
   /// Used to initialize the AutoRentalContext
   /// </summary>
   public static class AutoRentalContextInitializer
   {
      public static void Init()
      {
         /// DataDirectory is not currently being used to parametrize the .mdf location
         /// Leaving the line in place in case I elect to use it in the future
         /// Will/should delete once I iron out how to pull down db from Azure to local/dev and back
         AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());

         var migrator = new DbMigrator(new AutoRental.Data.Migrations.Configuration());
         migrator.Update();   //oco: runs migration on startup ..see Migrations/Configration.cs
      }
   }
}
