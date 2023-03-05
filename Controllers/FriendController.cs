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
    public class FriendController : ControllerBase
    {

        private readonly IFriendService friendService;

        public FriendController(IFriendService friendService)
        {
            this.friendService = friendService;
        }


        [HttpGet("getFriends/{id}")]
        public async Task<ActionResult<List<Friend>>> GetAllFriends(int id)
        {
            return await friendService.GetAllFriends(id);
        }




        [HttpGet("getInvites/{id}")]
        public async Task<ActionResult<List<Friend>>> GetAllInvites(int id)
        {
            return await friendService.GetInviteList(id);
        }



        [HttpPost]
        public async Task<ActionResult<Friend>> AddFriend(Friend friend)
        {
            try
            {
                if (friend == null)
                {
                    return BadRequest("Friend is null");
                }
                var friendToAdd = await friendService.AddFriend(friend);
                return Ok(friendToAdd);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("acceptFriend/{id}/{friendId}")]
        public async Task<ActionResult<Friend>> AcceptFriend(int id, int friendId)
        {
            try
            {
                if (friendId == 0)
                {
                    return BadRequest("Friend is null");
                }
                var friendToAdd = await friendService.AcceptFriend(id, friendId);
                return Ok(friendToAdd);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [HttpDelete("rejectFriend/{id}/{friendId}")]
        public async Task<ActionResult<Friend>> RejectFriend(int id, int friendId)
        {
            try
            {
                if (friendId == 0)
                {
                    return BadRequest("Friend is null");
                }
                var friendToAdd = await friendService.RejectFriend(id, friendId);
                return Ok(friendToAdd);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
