using Microsoft.EntityFrameworkCore;
using pantry_be.Models;

namespace pantry_be.Services
{
    public class AccountService: IAccountService
    {
        private readonly DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }




     
        public async Task<Account?> GetAccountById(int id)
        {
            var account = await _context.Accounts.Include(a => a.Users).Include(a => a.Friends).Include(a => a.Recipes).Include(a => a.Items)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (account is null)        
            {
                throw new Exception("Account not found");
            }

            return account;
        }



        public async Task AddAccount(Account account)
        {
            var emailExists = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == account.Email);
            if (emailExists is null)
            {
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return;
            }

            throw new Exception();

        }



        public async Task<Account?> GetAccountByEmailAndPassword(string email, string password)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(account => account.Email == email && account.Password == password);
            if (account != null)
                return account;

            throw new Exception("Username taken");
        }





        public async Task<List<Account>> GetNonFriendAccounts(int id)
        {
            var myAccount = await _context.Accounts
                .Include(a => a.Users)
                .Include(a => a.Friends)
                .Include(a => a.Recipes)
                .Include(a => a.Items)
                .FirstOrDefaultAsync(a => a.Id == id);

            var accounts = await _context.Accounts
                .Include(a => a.Users)
                .Include(a => a.Friends)
                .Include(a => a.Recipes)
                .Include(a => a.Items)
                .Where(a => a.Id != id)
                .ToListAsync();

            var friendsToExclude = await _context.Friends
                .Where(f => f.AccountId == id && f.MyFriend == true)
                .ToListAsync();

            if (myAccount.Friends.Any())
            {
                var friendIdsToExclude = friendsToExclude.Where(f => f.AccountId == id && f.MyFriend == true)
                    .Select(f => f.FriendId).ToList();

                accounts = accounts.Where(a => !friendIdsToExclude.Contains(a.Id)).ToList();
            }

            return accounts;
        }
    }
}
