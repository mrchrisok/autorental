using AutoRental.Business.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AutoRental.Data.Configurations
{
   public class AccountConfiguration : EntityTypeConfiguration<Account>
   {
      public AccountConfiguration()
      {
         HasKey<int>(x => x.AccountId);
      }
   }
}
