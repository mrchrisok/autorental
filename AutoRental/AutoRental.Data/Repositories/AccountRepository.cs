using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AutoRental.Data.Repositories
{
   [Export(typeof(IAccountRepository))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class AccountRepository : AutoRentalRepositoryBase<Account>, IAccountRepository
   {
      //------------------------------------------------------------------------------------
      #region Members.Override

      protected override Account AddEntity(AutoRentalContext entityContext, Account entity)
      {
         return entityContext.AccountSet.Add(entity);
      }

      protected override Account UpdateEntity(AutoRentalContext entityContext, Account entity)
      {
         return (from e in entityContext.AccountSet
               where e.AccountId == entity.AccountId
               select e).FirstOrDefault();
      }

      protected override IEnumerable<Account> GetEntities(AutoRentalContext entityContext)
      {
         return from e in entityContext.AccountSet
               select e;
      }

      protected override Account GetEntity(AutoRentalContext entityContext, int id)
      {
         var query = (from e in entityContext.AccountSet
                     where e.AccountId == id
                     select e);

         var results = query.FirstOrDefault();

         return results;
      }
      #endregion

      //------------------------------------------------------------------------------------
      #region Members.IAcountRepository

      public Account GetByLogin(string login)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
               return (from a in entityContext.AccountSet
                  where a.LoginEmail == login
                  select a).FirstOrDefault();
         }
      }
      #endregion
   }
}
