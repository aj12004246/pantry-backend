using Microsoft.EntityFrameworkCore;
using pantry_be.Models;

namespace pantry_be.Services
{
    public class RecipeService : IRecipeService
    {

        private readonly DataContext context;

        public RecipeService(DataContext context)
        {
            this.context = context;
        }


        public async Task<List<Recipe>> GetRecipes(int id)
        {
            var recipes = await context.Recipes.Include(r => r.Steps)
            .Where(r => r.AccountId == id)
            .ToListAsync();
            return recipes;
        }



        public async Task<Recipe?> AddRecipe(int accountId, Recipe recipe)
        {
            var newRecipe = await context.Recipes.FirstOrDefaultAsync(r => r.Name == recipe.Name && r.AccountId == accountId);
            var myFriends = await context.Friends.Include(f => f.FriendRecipes)
            .Where(f => f.FriendId == accountId && f.IsFriend == true)
            .ToListAsync();
            if (newRecipe == null)
            {
                newRecipe = new Recipe
                {
                    AccountId = accountId,
                    UserName = recipe.UserName,
                    Name = recipe.Name,
                    Img = recipe.Img,
                    Steps = new List<Step>()
                };
                context.Recipes.Add(newRecipe);
                await context.SaveChangesAsync();
                foreach (var friend in myFriends)
                {
                    friend.FriendRecipes.Add(newRecipe);
                }
                await context.SaveChangesAsync();
                return newRecipe;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteRecipe(int id)
        {
            var recipeSteps = await context.Steps.Where(s => s.RecipeId == id).ToListAsync();
            var recipeToDelete = await context.Recipes.FindAsync(id);
            if (recipeToDelete == null)
            {
                throw new Exception("Recipe not found");
            }
            context.Recipes.Remove(recipeToDelete);
            context.Steps.RemoveRange(recipeSteps);
            await context.SaveChangesAsync();
        }



    }
}
