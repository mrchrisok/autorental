using AutoRental.Business.Contracts;
using AutoRental.Business.Entities;
using AutoRental.Common;
using AutoRental.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System.ComponentModel.Composition;
using System.Security.Permissions;
using System.ServiceModel;

namespace AutoRental.Business.Managers
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
   public class AccountManager : ManagerBase, IAccountService
   {
      #region Constructors

      public AccountManager() { }

      public AccountManager(IDataRepositoryFactory dataRepositoryFactory)
      {
         _DataRepositoryFactory = dataRepositoryFactory;
      }
      #endregion

      //----------------------------------------------------------------------------------------
      #region Operations.IAccountService

      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public Account GetCustomerAccountInfo(string loginEmail)
      {
         return ExecuteFaultHandledOperation(() =>
         {
            Account accountEntity = GetAccountEntity(loginEmail);
            ValidateAuthorization(accountEntity);
            return accountEntity;
         });
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.Admin)]
      [PrincipalPermission(SecurityAction.Demand, Role = Security.User)]
      public void UpdateCustomerAccountInfo(Account account)
      {
         ExecuteFaultHandledOperation(() =>
         {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            ValidateAuthorization(account);
            Account updatedAccount = accountRepository.Update(account);
         });
      }
        #endregion

   }
}
