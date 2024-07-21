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
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutRep rep;
        private readonly DbContainer db;

        public WorkoutController(IWorkoutRep rep, DbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditWorkout([FromForm] AddWorkoutModel obj)
        {
            return Ok(rep.EditWorkout(obj));
        }


        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult GetWorkoutByToken(string Token)
        {
            return Ok(rep.GetWorkoutByToken(Token));
        }


        [Route("[controller]/[Action]/{UserId}/{WorkoutToken}")]
        [HttpGet]
        public IActionResult DeleteWorkout(string UserId, string WorkoutToken)
        {
            return Ok(rep.DeleteWorkout(UserId, WorkoutToken));
        }


        [Route("[controller]/[Action]/{Id}")]
        [HttpGet]
        public IActionResult GetMyWorkouts(string Id)
        {
            return Ok(rep.GetMyWorkouts(Id));
        }


        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult GetWorkoutThumbnail(string Token)
        {
            Workout obj = db.workout.Where(a => a.Token == Token).FirstOrDefault();
            if (obj.ThumbnailData == null || obj.ThumbnailExtension == "" || obj.ThumbnailExtension == null || obj.ThumbnailContentType == null)
            {
                var data = System.IO.File.ReadAllBytes(Path.Combine("wwwroot", "Thumbnail.png"));
                return File(data, "image/png", "Workout.png");
            }
            return File(obj.ThumbnailData, obj.ThumbnailContentType, obj.Name + "." + obj.ThumbnailExtension);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddWorkout([FromForm] AddWorkoutModel obj)
        {
            return Ok(rep.AddWorkout(obj));
        }
    }
}
