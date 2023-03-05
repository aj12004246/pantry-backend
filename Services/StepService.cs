using Microsoft.EntityFrameworkCore;
using pantry_be.Models;
using System.Runtime.InteropServices;

namespace pantry_be.Services
{
    public class StepService: IStepService
    {
        private readonly DataContext context;

        public StepService(DataContext context)
        {
            this.context = context;
        }

      
        public async Task AddStep(int recipeId, int itemId, int weight, int unit, Step step)
        {
            var findItem = await context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
            var newStep = await context.Steps.FirstOrDefaultAsync(s => s.Id == step.Id && s.RecipeId == recipeId);
            if (newStep == null)
            {
                if (findItem.Weight < weight * unit)
                {
                    throw new Exception("Item weight is less than the amount needed for this step");
                }
                else
                {
                    newStep = new Step
                    {
                        RecipeId = recipeId,
                        Description = step.Description,
                        ItemId = findItem.Id,
                        ItemName = findItem.Name,
                        ItemWeight = weight * unit,
                    };
                    context.Steps.Add(newStep);
                    findItem.Weight -= weight * unit;
                    await context.SaveChangesAsync();
                }
            }
            else
            {
                throw new Exception("Step number already exists");
            }
        }


        public async Task AddEStep(int recipeId, Step step)

        {
            var newStep = await context.Steps.FirstOrDefaultAsync(s => s.Id == step.Id && s.RecipeId == recipeId);
            if (newStep == null)
            {
                newStep = new Step
                {
                    RecipeId = recipeId,
                    Description = step.Description,
                    ItemId = null,
                    ItemName = null,
                    ItemWeight = null,
                };
                context.Steps.Add(newStep);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Step number already exists");

            }

        }


        public async Task<List<Step>> GetNewSteps(int recipeId)
        {
            var recipe = await context.Recipes.Include(r => r.Steps).FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe == null)
            {
                throw new Exception("steps not found");
            }
        
            var steps = recipe.Steps.ToList();

            return steps;
        }



        public async Task DeleteStep(int id)
        {
            var stepToDelete = await context.Steps.FindAsync(id);
            if (stepToDelete == null)
            {
                throw new Exception("Product not found");
            }
            context.Steps.Remove(stepToDelete);
            await context.SaveChangesAsync();
        }


        public async Task<List<Step>> GetFriendRecipeSteps(int id, int recipeId)
        {
            var friends = await context.Friends.Include(f => f.FriendRecipes)
                .ThenInclude(r => r.Steps)
                .Where(f => f.AccountId == id && f.IsFriend == true)
                .ToListAsync();
            var steps = new List<Step>();
            foreach (var friend in friends)
            {
                var friendRecipe = friend.FriendRecipes.FirstOrDefault(r => r.Id == recipeId);
                if (friendRecipe != null)
                {
                    steps.AddRange(friendRecipe.Steps);
                }
            }
            return steps;
        }


        public async Task<List<Step>> GetRecipeSteps(int recipeId)
        {
            var steps = await context.Steps    
                .Where(s => s.RecipeId == recipeId)             
                .ToListAsync();
                return steps;
        }


        public async Task<Step> UpdateEStep(int id, Step step)
        {
            var existingStep = await context.Steps.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStep != null)
            {
                existingStep.Description = step.Description;
                existingStep.ItemId = null;
                existingStep.ItemName = null;
                existingStep.ItemWeight = null;
                await context.SaveChangesAsync();
                return existingStep;
            }
            else
            {
                throw new Exception("Step not found");
            }
        }



        public async Task<Step> UpdateStep(int itemId, int unit, Step step)
        {
            var existingItem = await context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
            var existingStep = await context.Steps.FirstOrDefaultAsync(s => s.Id == step.Id);
            if (existingStep != null && existingItem != null)
            {
                existingStep.Description = step.Description;
                existingStep.ItemId = step.ItemId == 0 ? (int?)null : step.ItemId;
                existingStep.ItemName = existingItem?.Name;

                if (existingItem.Weight + existingStep.ItemWeight < step.ItemWeight)
                {
                    throw new Exception("Item weight is less than step weight");
                }

                existingItem.Weight += existingStep.ItemWeight?? 0;


                existingStep.ItemWeight = step.ItemWeight.HasValue ? step.ItemWeight.Value * unit : (int?)null;
                existingItem.Weight -= existingStep.ItemWeight?? 0;
                await context.SaveChangesAsync();
                return existingStep;
            }
            else
            {
                throw new Exception("Step not found");
            }
        }






       /* public async Task<Step> UpdateStep(int itemId, int unit, Step step)
        {
            var existingItem = await context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
            var existingStep = await context.Steps.FirstOrDefaultAsync(s => s.Id == step.Id);
            if (existingStep != null)
            {
                existingStep.Description = step.Description;
                existingStep.ItemId = step.ItemId == 0 ? (int?)null : step.ItemId;
                existingStep.ItemName = existingItem?.Name; ;

                int newWeight = step.ItemWeight.HasValue ? step.ItemWeight.Value * unit : 0;

                if (existingItem.Weight < newWeight)
                {
                    throw new Exception("Item weight is less than step weight");
                }

                existingItem.Weight += existingStep.ItemWeight ?? 0;
                existingStep.ItemWeight = newWeight;
                existingItem.Weight -= newWeight;
                await context.SaveChangesAsync();
                return existingStep;
            }
            else
            {
                throw new Exception("Step not found");
            }
        }

*/



    }
}
