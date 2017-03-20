using AutoRental.Business.Entities;
using Core.Common.Contracts;
using System.Collections.Generic;

namespace AutoRental.Data.Contracts
{
   public interface ISaleRepository : IDataRepository<Sale>
   {
      IEnumerable<Sale> GetSaleHistoryByVIN(string vin);
      Sale GetLastSaleByCar(int carId); 
      Sale GetLastSaleByVIN(string vin);
      IEnumerable<Sale> GetCurrentlySoldCars();
      IEnumerable<Sale> GetSaleHistoryByAccount(int accountId);
      IEnumerable<CustomerSaleInfo> GetCurrentCustomerSaleInfo();
   }
}
