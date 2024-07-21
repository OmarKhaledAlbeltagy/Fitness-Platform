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
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRep rep;

        public LanguageController(ILanguageRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllLanguages()
        {
            return Ok(rep.GetAllLanguages());
        }
    }
}
