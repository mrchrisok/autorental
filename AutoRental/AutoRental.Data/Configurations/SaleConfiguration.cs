using AutoRental.Business.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AutoRental.Data.Configurations
{
   public class SaleConfiguration : EntityTypeConfiguration<Sale>
   {
      public SaleConfiguration()
      {
         HasKey<int>(x => x.SaleId);
      }
   }
}
