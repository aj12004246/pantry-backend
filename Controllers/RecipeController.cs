using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pantry_be.Models;
using pantry_be.Services;

namespace pantry_be.Controllers
{
    [EnableCors("localhost")]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {

        private readonly IRecipeService recipeService;
        private readonly IFriendService friendService;

        public RecipeController(IRecipeService recipeService, IFriendService friendService)
        {
            this.recipeService = recipeService;
            this.friendService = friendService;
        }




        [HttpGet("friendRecipes/{id}/{friendId}")]
        public async Task<ActionResult<List<Recipe>>> GetFriendRecipes(int id, int friendId)
        {
            return await friendService.GetFriendRecipes(id, friendId);
        }




        [HttpPost("addRecipe/{accountId}")]
        public async Task<ActionResult<Recipe>> CreateRecipe(int accountId, Recipe recipe)
        {
            try
            {
                if (recipe == null)
                {
                    return BadRequest("Recipe is null");
                }
                var recipeToAdd = await recipeService.AddRecipe(accountId, recipe);
                if (recipeToAdd == null)
                    return BadRequest("Recipe already exists");
                else
                    return Ok(recipeToAdd);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipe(int id)
        {
            try
            {
                await recipeService.DeleteRecipe(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("getRecipes/{id}")]
        public async Task<ActionResult<List<Recipe>>> GetAllRecipes(int id)
        {
            return await recipeService.GetRecipes(id);
        }

    }
}
