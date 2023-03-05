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
    public class StepController : ControllerBase
    {
        private readonly IStepService stepService;

        public StepController(IStepService stepService)
        {
            this.stepService = stepService;
        }

      

        [HttpPost("addStep/{recipeId}/{itemId}/{weight}/{unit}")]
        public async Task<ActionResult<Step>> CreateStep(int recipeId, int itemId, int weight, int unit, Step step)
        {
            try
            {
                if (step == null)
                {
                    return BadRequest("Step is null");
                }
                await stepService.AddStep(recipeId, itemId, weight, unit, step);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpGet("newStep/{recipeId}")]
        public async Task<ActionResult<List<Step>>> GetAllSteps(int recipeId)
        {
            try
            {
                var steps = await stepService.GetNewSteps(recipeId);
                return Ok(steps);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("friendSteps/{id}/{recipeId}")]
        public async Task<ActionResult<List<Step>>> GetFriendRecipeSteps(int id, int recipeId)
        {
            try
            {
                var steps = await stepService.GetFriendRecipeSteps(id, recipeId);
                return Ok(steps);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }




        [HttpGet("getSteps/{id}")]
        public async Task<ActionResult<List<Step>>> GetSteps(int id)
        {
            return await stepService.GetRecipeSteps(id);
        }



        [HttpPut("{itemId}/{unit}")]
        public async Task<ActionResult<Item>> UpdateStep(int itemId, int unit, Step step)
        {
            try
            {
                if (step == null)
                {
                    return BadRequest("Product is null");
                }
                var stepToChange = await stepService.UpdateStep(itemId, unit, step);
                return Ok(stepToChange);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPut("editStep/{id}")]
        public async Task<ActionResult<Item>> UpdateEStep(int id, Step step)
        {
            try
            {
                if (step == null)
                {
                    return BadRequest("Product is null");
                }
                var stepToChange = await stepService.UpdateEStep(id, step);
                return Ok(stepToChange);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStep(int id)
        {
            try
            {
                await stepService.DeleteStep(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpPost("addStep/{recipeId}")]
        public async Task<ActionResult<Step>> CreateEStep(int recipeId, Step step)
        {
            try
            {
                if (step == null)
                {
                    return BadRequest("Step is null");
                }
                await stepService.AddEStep(recipeId, step);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

    }
}
