using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using Core.Common.Extensions;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AutoRental.Data.Repositories
{
   [Export(typeof(IRentalRepository))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class RentalRepository : AutoRentalRepositoryBase<Rental>, IRentalRepository
   {
      //--------------------------------------------------------------------------------------------
      #region Members.Override

      protected override Rental AddEntity(AutoRentalContext entityContext, Rental entity)
      {
         return entityContext.RentalSet.Add(entity);
      }

      protected override Rental UpdateEntity(AutoRentalContext entityContext, Rental entity)
      {
         return (from e in entityContext.RentalSet
               where e.RentalId == entity.RentalId
               select e).FirstOrDefault();
      }

      protected override IEnumerable<Rental> GetEntities(AutoRentalContext entityContext)
      {
         return from e in entityContext.RentalSet
               select e;
      }

      protected override Rental GetEntity(AutoRentalContext entityContext, int id)
      {
         var query = (from e in entityContext.RentalSet
                     where e.RentalId == id
                     select e);

         var results = query.FirstOrDefault();

         return results;
      }
      #endregion

      //--------------------------------------------------------------------------------------------
      #region Members.IRentalRepository

      public IEnumerable<Rental> GetRentalHistoryByCar(int carId)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from e in entityContext.RentalSet
                        where e.CarId == carId
                        select e;

            return query.ToFullyLoaded();
         }
      }

      public Rental GetCurrentRentalByCar(int carId)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from e in entityContext.RentalSet
                        where e.CarId == carId && e.DateReturned == null
                        select e;

            return query.FirstOrDefault();
         }
      }

      public IEnumerable<Rental> GetCurrentlyRentedCars()
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from e in entityContext.RentalSet
                        where e.DateReturned == null
                        select e;

            return query.ToFullyLoaded();
         }
      }

      public IEnumerable<Rental> GetRentalHistoryByAccount(int accountId)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from e in entityContext.RentalSet
                        where e.AccountId == accountId
                        select e;

            return query.ToFullyLoaded();
         }
      }

      public IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo()
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from r in entityContext.RentalSet
                        where r.DateReturned == null
                        join a in entityContext.AccountSet on r.AccountId equals a.AccountId
                        join c in entityContext.CarSet on r.CarId equals c.CarId
                        select new CustomerRentalInfo()
                        {
                           Customer = a,
                           Car = c,
                           Rental = r
                        };

            return query.ToFullyLoaded();
         }
      }

      public IEnumerable<CustomerRentalInfo> GetCustomerRentalInfoByAccount(int accountId)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from r in entityContext.RentalSet
                        where r.AccountId == accountId
                        join a in entityContext.AccountSet on r.AccountId equals a.AccountId
                        join c in entityContext.CarSet on r.CarId equals c.CarId
                        select new CustomerRentalInfo()
                        {
                           Customer = a,
                           Car = c,
                           Rental = r
                        };

            return query.ToFullyLoaded();
         }
      }
      #endregion
   }
}

