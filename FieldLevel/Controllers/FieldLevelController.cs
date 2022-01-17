using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FieldLevel.Models;
using Microsoft.AspNetCore.Server;

namespace FieldLevel.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldLevelController : Controller
    {
        //get most recent post per User
        [HttpGet("getposts")]
        public async Task<IActionResult> GetPosts(string postBackUrl)
        {
            //Posts.GetCurrentPosts();

            //return Ok(posts); //with Task<IActionResult> 

            //return Ok(StatusCode(202));//Status Code 202 = Accepted
        }
    }
}
