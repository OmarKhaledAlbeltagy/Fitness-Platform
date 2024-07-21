using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using Trainify.Context;
using Trainify.Privilage;
using Trainify.Repo;
using Trainify.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Trainify.Entities;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<ExtendIdentityRole> roleManager;
        private readonly UserManager<ExtendIdentityUser> userManager;
        private readonly DbContainer db;
        private readonly IUserRep rep;
        public UserController(RoleManager<ExtendIdentityRole> roleManager, UserManager<ExtendIdentityUser> userManager, DbContainer db, IUserRep rep)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.db = db;
            this.rep = rep;
        }

        [Route("[controller]/[Action]/{Id}")]
        [HttpGet]
        public IActionResult GetPostImage(int Id)
        {
            UserPost post = db.post.Find(Id);
            if (post.ImageData == null || post.ImageExtension == "" || post.ImageExtension == null || post.ImageContentType == null)
            {
                var data = System.IO.File.ReadAllBytes(Path.Combine("wwwroot", "EmptyCover.jpg"));
                return File(data, "image/jpeg", "Cover.jpg");
            }
            return File(post.ImageData, post.ImageContentType,"post." + post.ImageExtension);
        }

        [Route("[controller]/[Action]/{Id}")]
        [HttpGet]
        public IActionResult GetCoverPhoto(string Id)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(Id).Result;
            if (user.CoverData == null || user.CoverExtension == "" || user.CoverExtension == null || user.CoverContentType == null)
            {
                var data = System.IO.File.ReadAllBytes(Path.Combine("wwwroot", "EmptyCover.jpg"));
                return File(data, "image/jpeg", "Cover.jpg");
            }
            return File(user.CoverData, user.CoverContentType, user.FirstName + "." + user.CoverExtension);
        }

        [Route("[controller]/[Action]/{Id}")]
        [HttpGet]
        public IActionResult GetProfilePicture(string Id)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(Id).Result;
            if (user.ProfilePictureData == null || user.ProfilePictureExtension == "" || user.ProfilePictureExtension == null || user.ProfilePictureContentType == null)
            {
                var data = System.IO.File.ReadAllBytes(Path.Combine("wwwroot", "pp.png"));
                return File(data, "image/png", "Profile.png");
            }
            return File(user.ProfilePictureData, user.ProfilePictureContentType, user.FirstName + "." + user.ProfilePictureExtension);
        }

        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult GetGalleryImage(string Token)
        {
            UserGallery userGallery = db.userGallery.Where(a => a.Token == Token).First();
            
            return File(userGallery.GalleryImageData, userGallery.GalleryImageContentType,"Gallery." + userGallery.GalleryImageExtension);
        }



        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            List<ExtendIdentityRole> roles =  roleManager.Roles.ToList();
            return Ok(roles);
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetRegistrationRoles()
        {
            List<string> roles = db.Roles.Where(a => a.Name == "Trainer" || a.Name == "Client").Select(a=>a.Name).ToList();
            return Ok(roles);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]

        [Route("[controller]/[Action]/{email}")]
        [HttpGet]
        public IActionResult ResendConfirmationEmail(string email)
        {
            ExtendIdentityUser user = userManager.FindByEmailAsync(email).Result;
            string token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            string body = token;
            MailMessage m = new MailMessage();
            m.To.Add(user.Email);
            m.Subject = "Trainify Email Confirmation";
            m.From = new MailAddress("example@abc.com");
            m.Sender = new MailAddress("example@abc.com");
            m.IsBodyHtml = true;
            m.Body = body;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("SMTP-Server", 587);
            smtp.EnableSsl = false;
            smtp.Credentials = new NetworkCredential("example@abc.com", "Password");
            smtp.Send(m);
            return Ok(true);
        }

        [Route("[controller]/[Action]/{Id}/{Email}")]
        [HttpGet]
        public IActionResult ResendConfirmationChangeEmail(string Id, string Email)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(Id).Result;
            string token = userManager.GenerateChangeEmailTokenAsync(user, Email).Result;
            string body = token;
            MailMessage m = new MailMessage();
            m.To.Add(user.Email);
            m.Subject = "Trainify Email Confirmation";
            m.From = new MailAddress("example@abc.com");
            m.Sender = new MailAddress("example@abc.com");
            m.IsBodyHtml = true;
            m.Body = body;
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("SMTP-Server", 587);
            smtp.EnableSsl = false;
            smtp.Credentials = new NetworkCredential("example@abc.com", "Password");
            smtp.Send(m);
            return Ok(true);
        }

        [Route("[controller]/[Action]/{email}/{token}")]
        [HttpGet]
        public IActionResult ConfirmEmail(string email, string token)
        {
            ExtendIdentityUser user = userManager.FindByEmailAsync(email).Result;
            var x = userManager.ConfirmEmailAsync(user, token).Result;
            if (x.Succeeded)
            {
                return Ok(true);
            }
            else
            {
                return Ok("Invalid confirmation code");
            }

        }

        [Route("[controller]/[Action]/{Id}/{email}/{token}")]
        [HttpGet]
        public IActionResult ConfirmChangeEmail(string Id, string email, string token)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(Id).Result;
            var x =  userManager.ChangeEmailAsync(user, email, token).Result;

            if (x.Succeeded)
            {
                var y = userManager.SetUserNameAsync(user, email).Result;
                return Ok(true);
            }
            else
            {
                return Ok("Invalid confirmation code");
            }

        }

        [Route("[controller]/[Action]/{UserId}/{NewEmail}/{token}")]
        [HttpGet]
        public IActionResult ConfirmEmailChange(string UserId,string NewEmail, string token)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(UserId).Result;
            var x = userManager.ChangeEmailAsync(user, NewEmail, token).Result;
         
            if (x.Succeeded)
            {
                return Ok(true);
            }
            else
            {
                return Ok("Invalid confirmation code");
            }

        }

        [Route("[controller]/[Action]/{email=email}")]
        [HttpGet]
        public IActionResult CheckEmail([FromQuery] string email)
        {
            ExtendIdentityUser user = userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [Route("[controller]/[Action]/{PhoneNumber=PhoneNumber}")]
        [HttpGet]
        public IActionResult CheckPhone([FromQuery] string PhoneNumber)
        {
            if (PhoneNumber[0] == '0')
            {
                PhoneNumber = PhoneNumber.Remove(0, 1);
            }
            ExtendIdentityUser user = userManager.Users.Where(a => a.PhoneNumber == PhoneNumber).FirstOrDefault();
            if (user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [Route("[controller]/[Action]/{PhoneCodeId}/{Phonenumber}/{UserId}")]
        [HttpGet]
        public IActionResult CheckPhoneEdit(int PhoneCodeId, string PhoneNumber, string UserId)
        {
            if (PhoneNumber[0] == '0')
            {
                PhoneNumber = PhoneNumber.Remove(0, 1);
            }
            ExtendIdentityUser user = userManager.Users.Where(a => a.PhoneCodeId == PhoneCodeId && a.PhoneNumber == PhoneNumber && a.Id != UserId).FirstOrDefault();
            if (user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }


        [Route("[controller]/[Action]/{Email}/{UserId}")]
        [HttpGet]
        public IActionResult CheckEmailEdit(string Email, string UserId)
        {
            ExtendIdentityUser user = userManager.Users.Where(a => a.Email == Email && a.Id != UserId || a.UserName == Email && a.Id != UserId).FirstOrDefault();
            if (user == null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

    }
}
