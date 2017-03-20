using AutoRental.Business.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AutoRental.Data.Configurations
{
   public class CarConfiguration : EntityTypeConfiguration<Car>
   {
      public CarConfiguration()
      {
         HasKey<int>(x => x.CarId);

         Ignore(x => x.CurrentlyRented);
      }
   }
}
