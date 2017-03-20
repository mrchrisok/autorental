using Core.Common.Contracts;
using NP.Core.Common.Data;

namespace AutoRental.Data.Repositories
{
   /// <summary>
   /// Base class for repositories that read/write to AutoRentalContext
   /// </summary>
   /// <typeparam name="T">Entity that has a DbSet defined in AutoRentalContext</typeparam>
   public abstract class AutoRentalRepositoryBase<T> : DataRepositoryBase<T, AutoRentalContext>
      where T : class, IIdentifiableEntity, new()
   {
   }
}
