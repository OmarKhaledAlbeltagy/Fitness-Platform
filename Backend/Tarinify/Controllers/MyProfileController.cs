using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Trainify.Models;
using Trainify.Repo;
using Trainify.ViewModel;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class MyProfileController : ControllerBase
    {
        private readonly IMyProfileRep rep;

        public MyProfileController(IMyProfileRep rep)
        {
            this.rep = rep;
        }

        [Route("[controller]/[Action]/{UserId}")]
        [HttpGet]
        public IActionResult GetMySocialMedia(string UserId)
        {
            var res = rep.GetMySocialMedia(UserId);
            return Ok(res);
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UpdateMyCertificates(EditTrainerCertificateModel obj)
        {
            bool res = rep.UpdateMyCertificates(obj);
            return Ok(res);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UpdateBio(BioModel obj)
        {
            bool res = rep.UpdateBio(obj);
            return Ok(res);
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UpdateAccountSetting(AccountSettingModel obj)
        {
            bool res = rep.UpdateAccountSetting(obj);
            return Ok(res);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UpdatePassword(UpdatePasswordModel obj)
        {
            bool res = rep.UpdatePassword(obj);
            return Ok(res);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UpdateContactInfo(ContactInfoModel obj)
        {
            bool res = rep.UpdateContactInfo(obj);
            return Ok(res);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UpdateLocationInfo(LocationInfoModel obj)
        {
            bool res = rep.UpdateLocationInfo(obj);
            return Ok(res);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UpdateLanguages(LanguagesModel obj)
        {
            bool res = rep.UpdateLanguages(obj);
            return Ok(res);
        }

        [Route("[controller]/[Action]/{UserId}")]
        [HttpGet]
        public IActionResult GetMyProfile(string UserId)
        {
            MyProfileViewModel res = rep.GetMyProfile(UserId);
            return Ok(res);
        }

        [Route("[controller]/[Action]/{UserId}")]
        [HttpGet]
        public IActionResult GetMyProfile1(string UserId)
        {
            MyProfileViewModel1 res = rep.GetMyProfile1(UserId);
            return Ok(res);
        }

        [Route("[controller]/[Action]/{UserId}")]
        [HttpGet]
        public IActionResult GetMyProfile2(string UserId)
        {
            MyProfileViewModel2 res = rep.GetMyProfile2(UserId);
            return Ok(res);
        }

        [Route("[controller]/[Action]/{UserId}")]
        [HttpGet]
        public IActionResult GetMyPosts(string UserId)
        {
            var res = rep.GetMyPosts(UserId);
            return Ok(res);
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult ChangeCover([FromForm]UploadImageModel obj)
        {
            return Ok(rep.ChangeCover(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult UploadGalleryImage([FromForm] UploadImageModel obj)
        {
            return Ok(rep.UploadGalleryImage(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult ChangeProfilePicture([FromForm]UploadImageModel obj)
        {
            return Ok(rep.ChangeProfilePicture(obj));
        }
    }
}
