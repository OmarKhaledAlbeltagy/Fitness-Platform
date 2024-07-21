using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.Privilage;
using Trainify.Repo;
using Trainify.ViewModel;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class BMIController : ControllerBase
    {
        private readonly IBMICalcRep rep;
        private readonly DbContainer db;

        public BMIController(IBMICalcRep rep, DbContainer db)
        {
            this.rep = rep;
            this.db = db;
        }

       

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult GenerateNutritionPlan(GenerateNutritionModel obj)
        {
            return Ok(rep.GenerateNutrition(obj));
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetFoodMenu()
        {
            return Ok(rep.GetFoodMenu());
        }



        [Route("[controller]/[Action]/{Id}")]
        [HttpGet]
        public IActionResult GetFoodCategoryImage(int Id)
        {
            FoodCategory FC = db.foodCategory.Find(Id);
        
            return File(FC.ThumbnailData, FC.ThumbnailContentType, FC.CategoryName+"." + FC.ThumbnailExtension);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult CalculateBMI(BMIModel obj)
        {
            BMIViewModel res = rep.CalculateBMI(obj);
            return Ok(res);
        }
    }
}
