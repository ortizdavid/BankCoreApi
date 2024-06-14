using BankCoreApi.Models.Accounts;

namespace BankCoreApi.Repositories.Accounts
{
    public interface IAccountRepository<T> where T : class
    {
        Task<T?> GetByNubmerAsync(string? number);
        Task<T?> GetByIbanAsync(string iban);
        Task ChangeStatus(T account, AccountStatus newStatus);
    }
}