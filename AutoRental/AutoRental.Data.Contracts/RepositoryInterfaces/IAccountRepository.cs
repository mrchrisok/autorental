using AutoRental.Business.Entities;
using Core.Common.Contracts;

namespace AutoRental.Data.Contracts
{
    public interface IAccountRepository : IDataRepository<Account>
    {
        Account GetByLogin(string login);
    }
}
