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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUsers/{accountId}")]
        public async Task<ActionResult<List<User>>> GetAllUsers(int accountId)
        {
            try
            {
                var users = await _userService.GetAllUsers(accountId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


      
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var result = await _userService.GetUserById(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound("User not found.");
            }


        }

        [HttpPost("addUser/{accountId}")]
        public async Task<ActionResult> AddUser(int accountId, User user)
        {
            try
            {
                await _userService.AddUser(accountId, user);
                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

    }
}
