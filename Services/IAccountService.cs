using pantry_be.Models;

namespace pantry_be.Services
{
    public interface IAccountService
    {
        Task<List<Account>> GetNonFriendAccounts(int id);
        Task<Account?> GetAccountById(int id);
        Task AddAccount(Account account);
        Task<Account?> GetAccountByEmailAndPassword(string email, string password);
    }
}
