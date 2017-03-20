using AutoRental.Business.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AutoRental.Data.Configurations
{
   public class ReservationConfiguration : EntityTypeConfiguration<Reservation>
   {
      public ReservationConfiguration()
      {
         HasKey<int>(x => x.ReservationId);
      }
   }
}
