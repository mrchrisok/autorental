using AutoRental.Client.Contracts;
using AutoRental.Client.Entities;
using NP.Core.Common.ServiceModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace AutoRental.Client.Proxies
{
   [Export(typeof(IAccountService))]  // MefDI: interface mapping
   [PartCreationPolicy(CreationPolicy.NonShared)] // MEfDI: non-singleton
   public class AccountClient : UserClientBase<IAccountService>, IAccountService
   {
      //----------------------------------------------------------------------------
      #region Operations

      public Account GetCustomerAccountInfo(string loginEmail)
      {
         return Channel.GetCustomerAccountInfo(loginEmail);
      }

      public void UpdateCustomerAccountInfo(Account account)
      {
         Channel.UpdateCustomerAccountInfo(account);
      }
      #endregion
        
      //----------------------------------------------------------------------------
      #region Operations.Async

      public Task<Account> GetCustomerAccountInfoAsync(string loginEmail)
      {
         return Channel.GetCustomerAccountInfoAsync(loginEmail);
      }

      public Task UpdateCustomerAccountInfoAsync(Account account)
      {
         return Channel.UpdateCustomerAccountInfoAsync(account);
      }

      #endregion
   }
}
