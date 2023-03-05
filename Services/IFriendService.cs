using pantry_be.Models;

namespace pantry_be.Services
{
    public interface IFriendService
    {
        Task<List<Friend>> GetAllFriends(int id);
        Task<List<Friend>> GetInviteList(int id);
        Task<Friend> AddFriend( Friend friend);
        Task<Friend> AcceptFriend(int id, int friendId);
        Task<Friend> RejectFriend(int id, int friendId);
        Task<List<Recipe>> GetFriendRecipes(int id, int friendId);
    }
}
