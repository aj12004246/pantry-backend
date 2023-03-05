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
    public class ItemController : ControllerBase
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }




        [HttpGet("getItems/{accountId}")]
        public async Task<ActionResult<List<Item>>> GetAllItems(int accountId)
        {
            return Ok(await itemService.GetAllItems(accountId));
        }





        [HttpPost("addItem/{accountId}/{unit}")]
        public async Task<ActionResult<Item>> CreateItem(int accountId,int unit, Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Product is null");
                }
                var itemToAdd = await itemService.AddItem(accountId,unit, item);
                return Ok(itemToAdd);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }





        [HttpPut("{id}/{unit}")]
        public async Task<ActionResult<Item>> UpdateItem(int id, int unit, Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Product is null");
                }
                var itemToChange = await itemService.UpdateItem(id,unit,item);
                return Ok(itemToChange);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                await itemService.DeleteItem(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            try
            {
                var item = await itemService.GetItemById(id);
                return Ok(item);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
