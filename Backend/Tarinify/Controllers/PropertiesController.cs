using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.Repo;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertiesRep rep;
        private readonly DbContainer db;

        public PropertiesController(IPropertiesRep rep, DbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllTrainerTitles()
        {
            return Ok(rep.GetAllTrainerTitles());
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllWeightGoals()
        {
            return Ok(rep.GetAllWeightGoals());
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllBodyTypes()
        {
            return Ok(rep.GetAllBodyTypes());
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetAllActivityLevels()
        {
            return Ok(rep.GetAllActivityLevels());
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetBodyTypeImage(int id)
        {
            BodyType obj = db.bodyType.Find(id);
            return File(obj.ThumbnailData, obj.ThumbnailContentType/*, obj.BodyTypeName + "." + obj.ThumbnailExtension*/);
        }

        [Route("[controller]/[Action]/{id}")]
        [HttpGet]
        public IActionResult GetActivtyLevelImage(int id)
        {
            ActivityLevel obj = db.activityLevel.Find(id);
            return File(obj.ThumbnailData, obj.ThumbnailContentType/*, obj.ActivityLevelName + "." + obj.ThumbnailExtension*/);
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetTrainerCertificates()
        {
            List<string> res = db.trainerCertificate.Select(a=>a.CertificateName).ToList();
            return Ok(res);
        }
    }
}
