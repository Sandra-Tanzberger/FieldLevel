using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FieldLevel.Models;

namespace FieldLevel.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldLevelController : Controller
    {
        //get most recent post per User
        [HttpGet("getposts")]
        public IActionResult GetPosts(string postBackUrl)
        {
            //for test to see data
            List<Post> posts = Posts.GetCurrentPosts();

            return Ok(posts); //with Task<IActionResult> 

            //return Ok(StatusCode(202));//Status Code 202 = Accepted
        }

        [HttpPost("getpostbackurl")] //postback url 
        public IActionResult GetPostBackUrl([FromBody] List<Post> posts)
        {
            //write to postback data to file to test
            if (posts == null)
            {
                return BadRequest();
            }

            try
            {
                Posts.WritePostsToFile(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message.ToString() + "Inner Exception Error: " + ex.InnerException.Message.ToString());
            }

            return Ok();
        }
    }
}
