using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trainify.Privilage;
using Trainify.Repo;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRep rep;

        public CountryController(ICountryRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            return Ok(rep.GetAllCountries());
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllStates()
        {
            return Ok(rep.GetAllStates());
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetCountriesAndStates()
        {
            return Ok(rep.GetCountriesAndStates());
        }
    }
}
