using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using Core.Common.Extensions;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AutoRental.Data.Repositories
{
   [Export(typeof(ISaleRepository))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class SaleRepository : AutoRentalRepositoryBase<Sale>, ISaleRepository
   {
      //--------------------------------------------------------------------------------------------
      #region Members.Override

      protected override Sale AddEntity(AutoRentalContext entityContext, Sale entity)
      {
         return entityContext.SaleSet.Add(entity);
      }

      protected override Sale UpdateEntity(AutoRentalContext entityContext, Sale entity)
      {
         return (from e in entityContext.SaleSet
                 where e.SaleId == entity.SaleId
                 select e).FirstOrDefault();
      }

      protected override IEnumerable<Sale> GetEntities(AutoRentalContext entityContext)
      {
         return from e in entityContext.SaleSet
                select e;
      }

      protected override Sale GetEntity(AutoRentalContext entityContext, int id)
      {
         var query = (from e in entityContext.SaleSet
                      where e.SaleId == id
                      select e);

         var results = query.FirstOrDefault();

         return results;
      }
      #endregion

      //--------------------------------------------------------------------------------------------
      #region Members.ISaleRepository

      public IEnumerable<Sale> GetSaleHistoryByVIN(string vin)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from s in entityContext.SaleSet
                        join c in entityContext.CarSet on s.CarId equals c.CarId
                        where c.VIN == vin
                        select s;

            return query;
         }
      }

      public Sale GetLastSaleByCar(int carId)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from s in entityContext.SaleSet
                        where s.CarId == carId
                        orderby s.SaleDate descending
                        select s;

            return query.FirstOrDefault();
         }
      }

      public Sale GetLastSaleByVIN(string vin)
      {
         int carId;
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            carId = (from c in entityContext.CarSet
                    where c.VIN == vin
                    select c).FirstOrDefault().CarId;
         }

         return GetLastSaleByCar(carId);
      }

      public IEnumerable<Sale> GetCurrentlySoldCars()
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from s in entityContext.SaleSet
                        select s;

            return query.ToFullyLoaded();
         }
      }

      public IEnumerable<Sale> GetSaleHistoryByAccount(int accountId)
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from s in entityContext.SaleSet
                        where s.AccountId == accountId
                        select s;

            return query.ToFullyLoaded();
         }
      }

      public IEnumerable<CustomerSaleInfo> GetCurrentCustomerSaleInfo()
      {
         using (AutoRentalContext entityContext = new AutoRentalContext())
         {
            var query = from s in entityContext.SaleSet
                        join a in entityContext.AccountSet on s.AccountId equals a.AccountId
                        join c in entityContext.CarSet on s.CarId equals c.CarId
                        select new CustomerSaleInfo()
                        {
                           Customer = a,
                           Car = c,
                           Sale = s
                        };

            return query.ToFullyLoaded();
         }
      }
      #endregion
   }
}