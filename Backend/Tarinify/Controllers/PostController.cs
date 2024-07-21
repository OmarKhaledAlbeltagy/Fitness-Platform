using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainify.Models;
using Trainify.Repo;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class PostController : ControllerBase
    {
        private readonly IPostRep rep;

        public PostController(IPostRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddPost([FromForm] AddPostModel obj)
        {
            return Ok(rep.AddPost(obj));
        }

        [Route("[controller]/[Action]/{UserId}/{Token}")]
        [HttpGet]
        public IActionResult DeleteMyPost(string UserId, string Token)
        {
            return Ok(rep.DeleteMyPost(UserId, Token));
        }
    }
}
