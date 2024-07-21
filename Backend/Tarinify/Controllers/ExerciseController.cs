using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainify.Models;
using Trainify.Repo;
using Trainify.Context;
using Trainify.Entities;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using Microsoft.Graph.Models;
using System.Runtime.CompilerServices;
using Xabe.FFmpeg;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRep rep;
        private readonly DbContainer db;

        public ExerciseController(IExerciseRep rep, DbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }

        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult GetExerciseByToken(string Token)
        {
            return Ok(rep.GetExerciseByToken(Token));
        }


        [Route("[controller]/[Action]/{Id}")]
        [HttpGet]
        public IActionResult GetMyExercises(string Id)
        {
            return Ok(rep.GetMyExercises(Id));
        }


        [Route("[controller]/[Action]/{UserId}/{ExerciseToken}")]
        [HttpGet]
        public IActionResult DeleteExercise(string UserId, string ExerciseToken)
        {
            return Ok(rep.DeleteExercise(UserId, ExerciseToken));
        }


        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult GetExerciseThumbnail(string Token)
        {
            Exercise obj = db.exersice.Where(a=>a.Token == Token).FirstOrDefault();
            if (obj.ThumbnailData == null || obj.ThumbnailExtension == "" || obj.ThumbnailExtension == null || obj.ThumbnailContentType == null)
            {
                var data = System.IO.File.ReadAllBytes(Path.Combine("wwwroot", "Thumbnail.png"));
                return File(data, "image/png", "Workout.png");
            }
            return File(obj.ThumbnailData, obj.ThumbnailContentType, obj.Name + "." + obj.ThumbnailExtension);
        }


        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public async Task<IActionResult> GetExerciseExample(string Token)
        {
            Exercise obj = db.exersice.Where(a => a.Token == Token).FirstOrDefault();
            return File(obj.ExampleData,obj.ExampleContentType,obj.Name+"."+obj.ExampleExtension);
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddExercise([FromForm] AddExersiceModel obj)
        {
            return Ok(rep.AddExercise(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditExercise([FromForm] AddExersiceModel obj)
        {
            return Ok(rep.EditExercise(obj));
        }
    }
}
