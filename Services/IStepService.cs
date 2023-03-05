using pantry_be.Models;

namespace pantry_be.Services
{
    public interface IStepService
    {

        Task AddStep(int recipeId, int itemId, int weight, int unit, Step step);

        Task AddEStep(int recipeId, Step step);
        Task<Step> UpdateEStep(int id, Step step);
        Task<Step> UpdateStep(int itemId, int unit, Step step);
        Task DeleteStep(int id);
        Task<List<Step>> GetNewSteps(int recipeId);
       Task<List<Step>> GetFriendRecipeSteps(int id, int recipeId);
        Task<List<Step>> GetRecipeSteps(int recipeId);
    }
}
