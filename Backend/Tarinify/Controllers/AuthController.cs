using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trainify.Models;
using Trainify.Privilage;
using Trainify.Repo;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRep rep;
        private readonly UserManager<ExtendIdentityUser> userManager;

        public AuthController(IAuthRep rep, UserManager<ExtendIdentityUser> userManager)
        {
            this.rep = rep;
            this.userManager = userManager;
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult Login(LoginModel obj)
        {
            return Ok(rep.Login(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult ClientRegistration(ClientRegisterModel obj)
        {
            return Ok(rep.ClientRegistration(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult TrainerRegistration([FromForm]TrainerRegistrationModel obj)
        {
            return Ok(rep.TrainerRegistration(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult SignupGeneralInfo(SignupGeneralInfoModel obj)
        {
            return Ok(rep.SignupGeneralInfo(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult ConfirmEmail(EmailConfirmationModel obj)
        {
            return Ok(rep.ConfirmEmail(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetSignupRoles()
        {
            return Ok(rep.GetSignupRoles());
        }

        [Route("[controller]/[Action]/{email}")]
        [HttpGet]
        public async Task<IActionResult> SendEmailConfirmation(string email)
        {
           var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return Ok(rep.SendConfirmationEmail(user));
            }
            else
            {
                return BadRequest("User not found");
            }
        }
    }
}
