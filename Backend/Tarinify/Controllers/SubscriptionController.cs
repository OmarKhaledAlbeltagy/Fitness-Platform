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
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRep rep;

        public SubscriptionController(ISubscriptionRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult ClientSubscripePlan(ClientSubscriptionModel obj)
        {
            return Ok(rep.ClientSubscripePlan(obj));
        }

        [Route("[controller]/[Action]/{UserId}")]
        [HttpGet]
        public IActionResult GetTrainerPlans(string UserId)
        {
            return Ok(rep.GetTrainerPlans(UserId));
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllTrainers()
        {
            return Ok(rep.GetAllTrainers());
        }
    }
}
