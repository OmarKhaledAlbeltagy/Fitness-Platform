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
    public class NutritionController : ControllerBase
    {
        private readonly INutritionRep rep;
        private readonly DbContainer db;

        public NutritionController(INutritionRep rep, DbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddCustomFood([FromForm] AddCustomFoodModel obj)
        {
            return Ok(rep.AddCustomFood(obj));
        }


        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult GetFoodImage(string Token)
        {
            Food obj = db.food.Where(a => a.Token == Token).First();
            if (obj.ThumbnailData == null || obj.ThumbnailExtension == "" || obj.ThumbnailExtension == null || obj.ThumbnailContentType == null)
            {
                var data = System.IO.File.ReadAllBytes(Path.Combine("wwwroot", "MealThumb.png"));
                return File(data, "image/png", obj.Name+".png");
            }
            return File(obj.ThumbnailData, obj.ThumbnailContentType, obj.Name + "." + obj.ThumbnailExtension);
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddNutritionPlan(AddNutritionPlanModel obj)
        {
            return Ok(rep.AddNutritionPlan(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPatch]
        public IActionResult AssignNutritionForClient(AssignNutritionPlanModel obj)
        {
            return Ok(rep.AssignNutritionForClient(obj));
        }


        [Route("[controller]/[Action]/{Token}/{TrainerId}")]
        [HttpDelete]
        public IActionResult DeletePlan(string Token, string TrainerId)
        {
            return Ok(rep.DeletePlan(Token, TrainerId));
        }


        [Route("[controller]/[Action]")]
        [HttpPut]
        public IActionResult EditNutritionPlan(AddNutritionPlanModel obj)
        {
            return Ok(rep.EditNutritionPlan(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult GenerateAutoNutritionPlan(GenerateNutritionModel obj)
        {
            return Ok(rep.GenerateNutrition(obj));
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetFoodCategories()
        {
            return Ok(rep.GetFoodCategories());
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetFoodMenu()
        {
            return Ok(rep.GetFoodMenu());
        }


        [Route("[controller]/[Action]/{TrainerId}")]
        [HttpGet]
        public IActionResult GetTrainerPlans(string TrainerId)
        {
            return Ok(rep.GetTrainerPlans(TrainerId));
        }


        [Route("[controller]/[Action]/{Token}")]
        [HttpGet]
        public IActionResult TrainerSingleNutritionPlan(string PlanToken)
        {
            return Ok(rep.TrainerSingleNutritionPlan(PlanToken));
        }


        [Route("[controller]/[Action]/{PlanToken}")]
        [HttpGet]
        public IActionResult DuplicateNutritionPlan(string PlanToken)
        {
            return Ok(rep.DuplicateNutritionPlan(PlanToken));
        }

    }
}
