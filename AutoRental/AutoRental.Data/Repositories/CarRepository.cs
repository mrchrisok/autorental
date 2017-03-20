using AutoRental.Business.Entities;
using AutoRental.Data.Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AutoRental.Data.Repositories
{
   [Export(typeof(ICarRepository))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class CarRepository : AutoRentalRepositoryBase<Car>, ICarRepository
   {
      //-------------------------------------------------------------------------------
      #region Members.Override

      protected override Car AddEntity(AutoRentalContext entityContext, Car entity)
      {
         return entityContext.CarSet.Add(entity);
      }

      protected override Car UpdateEntity(AutoRentalContext entityContext, Car entity)
      {
         return (from e in entityContext.CarSet
               where e.CarId == entity.CarId
               select e).FirstOrDefault();
      }

      protected override IEnumerable<Car> GetEntities(AutoRentalContext entityContext)
      {
         return from e in entityContext.CarSet
               select e;
      }

      protected override Car GetEntity(AutoRentalContext entityContext, int id)
      {
         var query = (from e in entityContext.CarSet
                     where e.CarId == id
                     select e);

         var results = query.FirstOrDefault();

         return results;
      }
      #endregion
   }
}