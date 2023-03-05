using pantry_be.Models;

namespace pantry_be.Services
{
    public interface IRecipeService
    {
        Task<Recipe> AddRecipe(int accountId,Recipe recipe);

        Task<List<Recipe>> GetRecipes(int id);

        Task DeleteRecipe(int id);
    }
}
