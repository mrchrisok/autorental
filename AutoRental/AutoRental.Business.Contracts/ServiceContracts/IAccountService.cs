using AutoRental.Business.Entities;
using AutoRental.Common;
using Core.Common.Exceptions;
using System.ServiceModel;

namespace AutoRental.Business.Contracts
{
   [ServiceContract]
   public interface IAccountService
   {
      [OperationContract]
      [FaultContract(typeof(NotFoundException))]
      [FaultContract(typeof(AuthorizationValidationException))]
      Account GetCustomerAccountInfo(string loginEmail);

      [OperationContract]
      [FaultContract(typeof(AuthorizationValidationException))]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      void UpdateCustomerAccountInfo(Account account);
   }
}
