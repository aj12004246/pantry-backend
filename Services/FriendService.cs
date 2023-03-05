using Microsoft.EntityFrameworkCore;
using pantry_be.Models;

namespace pantry_be.Services
{
    public class FriendService : IFriendService
    {

        private readonly DataContext context;

        public FriendService(DataContext context)
        {
            this.context = context;
        }


        public async Task<List<Friend>> GetInviteList(int id)
        {
            var inviteList = await context.Friends.Where(f => f.AccountId == id && f.IsFriend == false)
                .ToListAsync();
            return inviteList;
        }


        public async Task<List<Friend>> GetAllFriends(int id)
        {
            var friendsList = await context.Friends.Where(f => f.AccountId == id && f.IsFriend == true)
                .ToListAsync();
            return friendsList;
        }




        public async Task<List<Recipe>> GetFriendRecipes(int id, int friendId)
        {
            var friend = await context.Friends
            .Include(f => f.FriendRecipes)
            .FirstOrDefaultAsync(f => f.AccountId == id && f.FriendId == friendId);
            var friendRecipes = new List<Recipe>();
            if (friend != null && friend.FriendRecipes.Any())
            {
                friendRecipes.AddRange(friend.FriendRecipes);
            }
            return friendRecipes;
        }



        /*   public async Task<List<Recipe>> GetFriendRecipes(int id)
           {
               var account = await context.Accounts
                   .Include(a => a.Friends)
                   .ThenInclude(f => f.FriendRecipes)
                   .FirstOrDefaultAsync(a => a.Id == id);

               if (account == null)
                   return new List<Recipe>();

               var friendRecipes = account.Friends.SelectMany(f => f.FriendRecipes).ToList();

               return friendRecipes;
           }*/


        public async Task<Friend> AddFriend(Friend friend)
        {
            var account = await context.Accounts
            .Include(a => a.Users)
            .Include(a => a.Friends)
            .Include(a => a.Recipes)
            .FirstOrDefaultAsync(a => a.Id == friend.AccountId);

        var friendAccount = await context.Accounts
            .Include(a => a.Users)
            .Include(a => a.Friends)
            .Include(a => a.Recipes)
            .FirstOrDefaultAsync(a => a.Id == friend.FriendId);

       var myRecipes = await context.Friends
             .Include(a => a.FriendRecipes)
             .FirstOrDefaultAsync(f => f.AccountId == friend.FriendId && f.FriendId == friend.AccountId);


            if (myRecipes == null)
            {
                myRecipes = new Friend
                {
                    AccountId = friend.FriendId,
                    FriendId = friend.AccountId,
                    Name = account.Name,
                    MyFriend = true,
                    IsFriend = false,
                    FriendRecipes = account.Recipes
                };

                friendAccount.Friends.Add(myRecipes);
                await context.SaveChangesAsync();
                return myRecipes;
            }
            else
            {
                throw new Exception("Already a friend");
            }

            
        }



        public async Task<Friend> AcceptFriend(int id, int friendId)
        {
            var account = await context.Accounts
            .Include(a => a.Users)
            .Include(a => a.Friends)
            .Include(a => a.Recipes)
            .FirstOrDefaultAsync(a => a.Id == id);

            var friendAccount = await context.Accounts
                .Include(a => a.Users)
                .Include(a => a.Friends)
                .Include(a => a.Recipes)
                .FirstOrDefaultAsync(a => a.Id == friendId);

            var myRecipes = await context.Friends
                  .Include(a => a.FriendRecipes)
                  .FirstOrDefaultAsync(f => f.AccountId == friendId && f.FriendId == id);

            if (myRecipes == null)
            {
                myRecipes = new Friend
                {
                    AccountId = friendId,
                    FriendId = id,
                    Name = account.Name,
                    MyFriend = true,
                    IsFriend = true,
                    FriendRecipes = account.Recipes
                };

                friendAccount.Friends.Add(myRecipes);
                var accepted = await context.Friends.FirstOrDefaultAsync(f => f.AccountId == id && f.FriendId == friendId);
                accepted.IsFriend = true;
                await context.SaveChangesAsync();
                return myRecipes;
            }
            else
            {
                throw new Exception("Already a friend");
            }


        }



      /*  public async Task<Friend> RejectFriend(int id, int friendId)
        {
            var account = await context.Accounts
            .Include(a => a.Users)
            .Include(a => a.Friends)
            .Include(a => a.Recipes)
            .FirstOrDefaultAsync(a => a.Id == id);

            var friendRequestAccount = await context.Accounts
                .Include(a => a.Users)
                .Include(a => a.Friends)
                .Include(a => a.Recipes)
                .FirstOrDefaultAsync(a => a.Id == friendId);

            var otherRecipes = await context.Friends
                  .Include(a => a.FriendRecipes)
                  .FirstOrDefaultAsync(f => f.AccountId == id && f.FriendId == friendId);

            if (otherRecipes != null)
            {
                otherRecipes = new Friend
                {
                    AccountId = id,
                    FriendId = friendId,
                    Name = friendRequestAccount.Name,
                    MyFriend = true,
                    IsFriend = false,
                    FriendRecipes = friendRequestAccount.Recipes
                };      
                await context.SaveChangesAsync();
                return otherRecipes;
            }
            else
            {
                throw new Exception("Already a friend");
            }


        }*/



        public async Task<Friend> RejectFriend(int id, int friendId)
        {

            var friendToDelete = await context.Friends
            .Include(a => a.FriendRecipes)
            .FirstOrDefaultAsync(f => f.AccountId == id && f.FriendId == friendId);

            if (friendToDelete != null)
            {
                context.Friends.Remove(friendToDelete);
                await context.SaveChangesAsync();
                return friendToDelete;
            }
            else
            {
                throw new Exception("not there");
            }
        }


    }
}
