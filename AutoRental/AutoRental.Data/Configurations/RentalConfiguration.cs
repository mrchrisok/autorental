using AutoRental.Business.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AutoRental.Data.Configurations
{
   public class RentalConfiguration : EntityTypeConfiguration<Rental>
   {
      public RentalConfiguration()
      {
         HasKey<int>(x => x.RentalId);
      }
   }
}
