using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainify.Repo;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaRep rep;

        public SocialMediaController(ISocialMediaRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllSocialMedia()
        {
            return Ok(rep.GetAllSocialMedia());
        }
    }
}
