using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using pantry_be.Models;

namespace pantry_be.Services
{
    public class UserService: IUserService
    {
            private readonly DataContext _context;

            public UserService(DataContext context)
            {
                _context = context;
            }


          
            public async Task<List<User>> GetAllUsers(int accountId)
        {
            var account = await _context.Accounts.Include(a => a.Friends).Include(a => a.Items).Include(a => a.Users).FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            var users = account.Users.ToList();

            return users;
        }

   
        public async Task<User?> GetUserById(int id)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user is null)
                {
                    throw new Exception("Account not found");
                }

                return user;
            }

            public async Task AddUser(int accountId, User user)
            {
                var userExists = await _context.Users.FirstOrDefaultAsync(u => u.AccountId == user.AccountId && u.Name == user.Name);
                if (userExists is null)
                {
                    _context.Users.Add(user);
                    user.AccountId = accountId;
                    await _context.SaveChangesAsync();
                    return;
                }

                throw new Exception();

            }

          
        
    }
}
