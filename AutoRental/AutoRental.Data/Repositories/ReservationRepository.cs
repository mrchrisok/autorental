using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AutoRental.Data.Repositories
{
   [Export(typeof(IReservationRepository))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class ReservationRepository : AutoRentalRepositoryBase<Reservation>, IReservationRepository
   {
      #region Members.Override

      protected override Reservation AddEntity(AutoRentalContext entityContext, Reservation entity)
      {
         return entityContext.ReservationSet.Add(entity);
      }

      protected override Reservation UpdateEntity(AutoRentalContext entityContext, Reservation entity)
      {
         return (from e in entityContext.ReservationSet
                  where e.ReservationId == entity.ReservationId
                  select e).FirstOrDefault();
      }

      protected override IEnumerable<Reservation> GetEntities(AutoRentalContext entityContext)
      {
         return from e in entityContext.ReservationSet
                  select e;
      }

      protected override Reservation GetEntity(AutoRentalContext entityContext, int id)
      {
         var query = (from e in entityContext.ReservationSet
                     where e.ReservationId == id
                     select e);

         var results = query.FirstOrDefault();

         return results;
      }
      #endregion

      //---------------------------------------------------------------------------------------------------
      #region Members.IReservationRepository

      public IEnumerable<CustomerReservationInfo> GetCurrentCustomerReservationInfo()
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from r in entityContext.ReservationSet
                        join a in entityContext.AccountSet on r.AccountId equals a.AccountId
                        join c in entityContext.CarSet on r.CarId equals c.CarId
                        select new CustomerReservationInfo()
                        {
                           Customer = a,
                           Car = c,
                           Reservation = r
                        };

            return query.ToFullyLoaded();
         }
      }

      public IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from r in entityContext.ReservationSet
                        where r.RentalDate < pickupDate
                        select r;

            return query.ToFullyLoaded();
         }
      }

      public IEnumerable<CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from r in entityContext.ReservationSet
                        join a in entityContext.AccountSet on r.AccountId equals a.AccountId
                        join c in entityContext.CarSet on r.CarId equals c.CarId
                        where r.AccountId == accountId
                        select new CustomerReservationInfo()
                        {
                           Customer = a,
                           Car = c,
                           Reservation = r
                        };

            return query.ToFullyLoaded();
         }
      }
      #endregion
   }
}
