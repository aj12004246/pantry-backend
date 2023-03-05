using pantry_be.Models;

namespace pantry_be.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers(int accountId);
        Task<User?> GetUserById(int id);
        Task AddUser(int accountId, User user);
    }
}
