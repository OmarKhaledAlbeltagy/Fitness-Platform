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
    public class MealController : ControllerBase
    {
        private readonly IMealRep rep;
        private readonly DbContainer db;

        public MealController(IMealRep rep, DbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }

        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult GetMealThumbnail(string Token)
        {
            Food obj = db.food.Where(a => a.Token == Token).FirstOrDefault();
            if (obj.ThumbnailData == null || obj.ThumbnailExtension == "" || obj.ThumbnailExtension == null || obj.ThumbnailContentType == null)
            {
                var data = System.IO.File.ReadAllBytes(Path.Combine("wwwroot", "MealThumb.png"));
                return File(data, "image/png", "Meal.png");
            }
            return File(obj.ThumbnailData, obj.ThumbnailContentType, obj.Name + "." + obj.ThumbnailExtension);
        }

        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult GetMealByToken(string Token)
        {
           return Ok(rep.GetMealByToken(Token));
        }

        [Route("[controller]/[Action]/{Id}")]
        [HttpGet]
        public IActionResult GetMyMeals(string Id)
        {
            return Ok(rep.GetMyMeals(Id));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddMeal([FromForm]AddMealModel obj)
        {
            return Ok(rep.AddMeal(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult EditMeal([FromForm]AddMealModel obj)
        {
            return Ok(rep.EditMeal(obj));
        }

        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult DeleteMeal(string Token)
        {
            return Ok(rep.DeleteMeal(Token));
        }
    }
}
