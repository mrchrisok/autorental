using AutoRental.Client.Entities;
using AutoRental.Common;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AutoRental.Client.Contracts
{
   [ServiceContract]
   public interface IAccountService : IServiceContract
   {
      #region Operations

      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      Account GetCustomerAccountInfo(string loginEmail);

      [OperationContract]
      [FaultContract(typeof(AuthorizationValidationException))]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      void UpdateCustomerAccountInfo(Account account);
      #endregion

      //--------------------------------------------------------------------------------------
      #region Operations.Async

      [OperationContract]
      Task<Account> GetCustomerAccountInfoAsync(string loginEmail);

      [OperationContract]
      Task UpdateCustomerAccountInfoAsync(Account account);
      #endregion
   }
}
