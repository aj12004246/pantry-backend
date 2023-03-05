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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


       
        [HttpGet("getAccounts/{id}")]
        public async Task<ActionResult<List<Account>>> GetNonFriendAccounts(int id)
        {
            return await _accountService.GetNonFriendAccounts(id);
        }


     
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            try
            {
                var result = await _accountService.GetAccountById(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound("Account not found.");
            }


        }

        [HttpPost]
        public async Task<ActionResult> AddAccount(Account account)
        {
            try
            {
                await _accountService.AddAccount(account);
                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<Account>> GetAccountbyEmailAndPassword(string email, string password)
        {
            try
            {
                var result = await _accountService.GetAccountByEmailAndPassword(email, password);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound("Username/Password Incorrect");
            }


        }
    }
}
