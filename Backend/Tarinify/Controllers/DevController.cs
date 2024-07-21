using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Functions.ImConjugate;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.Privilage;
using Trainify.Repo;

namespace Trainify.Controllers
{
    [EnableCors("allow")]
    [ApiController]
    [AllowAnonymous]
    public class DevController : ControllerBase
    {
        private readonly RoleManager<ExtendIdentityRole> roleManager;
        private readonly UserManager<ExtendIdentityUser> userManager;
        private readonly DbContainer db;
        private readonly IDevRep rep;

        public DevController(RoleManager<ExtendIdentityRole> roleManager, UserManager<ExtendIdentityUser> userManager, DbContainer db, IDevRep rep)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.db = db;
            this.rep = rep;
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public async Task<IActionResult> ChangeRole()
        {
            ExtendIdentityRole role = db.Roles.Find("4d6d142b-51c0-4acd-831f-45d2236e62c2");
            role.Name = "Client";
            role.NormalizedName = "CLIENT";
            db.SaveChanges();
            return Ok(true);
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddActivityLevel([FromForm] AddActivityLevelModel d)
        {
            return Ok(rep.AddActivityLevel(d));
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddBodyType([FromForm] AddBodyTypeModel d)
        {
            return Ok(rep.AddBodyType(d));
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult GetDateTime()
        {
            return Ok(rep.GetDateTime());
        }

        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult GetRedirectingUrl(DelStringModel obj)
        {
            return Ok(rep.GetRedirectingUrl(obj.x));
        }

            [Route("[controller]/[Action]")]
        [HttpPost]
        public async Task<IActionResult> Reviews(DeleteQuery obj)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.app.outscraper.com/maps/reviews-v3?query="+obj.Link+ "&reviewsLimit=20&apiKey=Z29vZ2xlLW9hdXRoMnwxMDQyODk4MzY2NDQxODgyMDI1ODl8ZmJjYzAzYTQxNA&reviewsQuery="+obj.Review);
            client.DefaultRequestHeaders.Add("X-API-KEY", "Z29vZ2xlLW9hdXRoMnwxMDQyODk4MzY2NDQxODgyMDI1ODl8ZmJjYzAzYTQxNA");
            
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get,"");

            var response = await client.SendAsync(httpRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            return new ContentResult()
            {
                Content = responseContent,
                ContentType = "application/json"
            };
        }


        [Route("[controller]/[Action]")]
        [HttpPost]
        public IActionResult AddFoodTags(List<DeleteAddFoodTagsModel> list)
        {
            foreach (var item in list)
            {
                Food f = new Food();
                f.FoodCategoryId = item.CategoryId;
                f.Name = item.Name;
                f.Calories = item.Calories;
                f.Protein = item.Protein;
                f.Carb = item.Carb;
                f.Fats = item.Fats;
                db.food.Add(f);
                db.SaveChanges();
                FoodTags ft = new FoodTags();
                ft.FoodId = f.Id;
                ft.Tag = item.Tag;
                db.foodTags.Add(ft);
                db.SaveChanges();
            }
            return Ok(true);
        }




        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult MealTypes()
        {
            List<Food> FoodList = db.food.ToList();
            foreach (var item in FoodList)
            {

                string type1 = item.Name.Split(' ')[1];
                string type2 = type1.Remove(type1.Length - 1);
                FoodMealTypes FMT = new FoodMealTypes();
                switch (type2)
                {
                    case "Breakfast":

                        FMT.MealTypesId = 1;
                        FMT.FoodId = item.Id;
                        db.foodMealTypes.Add(FMT);
                        break;
                    case "Lunch":

                        FMT.MealTypesId = 2;
                        FMT.FoodId = item.Id;
                        db.foodMealTypes.Add(FMT);
                        break;
                    case "Dinner":

                        FMT.MealTypesId = 3;
                        FMT.FoodId = item.Id;
                        db.foodMealTypes.Add(FMT);
                        break;
                }
            }
            db.SaveChanges();
            return Ok(true);
        }


        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult AddFoodImage([FromForm] int id, [FromForm] IFormFile file)
        {
            FoodCategory res = db.foodCategory.Find(id);
            var ThumbStream = new MemoryStream();
            file.CopyTo(ThumbStream);
            var ThumbBytes = ThumbStream.ToArray();
            res.ThumbnailData = ThumbBytes;
            res.ThumbnailContentType = file.ContentType;
            res.ThumbnailExtension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            ThumbStream.Dispose();
            db.SaveChanges();
            return Ok(true);
        }


        //[Route("[controller]/[Action]")]
        //[HttpPost]
        //public IActionResult AddFood(List<Food> list)
        //{
        //    db.food.AddRange(list);
        //    db.SaveChanges();
        //    return Ok(true);
        //}

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult Duplicate()
        {
            UserGallery c = db.userGallery.First();
            UserGallery n = new UserGallery();
            n.ExtendIdentityUserId = c.ExtendIdentityUserId;
            n.UploadDateTime = c.UploadDateTime;
            n.GalleryImageData = c.GalleryImageData;
            n.GalleryImageExtension = c.GalleryImageExtension;
            n.GalleryImageContentType = c.GalleryImageContentType;
            n.Token = Guid.NewGuid().ToString().Replace("-", string.Empty);



            db.userGallery.Add(n);
            db.userGallery.Add(n);
            db.userGallery.Add(n);
            db.userGallery.Add(n);
            db.userGallery.Add(n);
            db.userGallery.Add(n);
            db.userGallery.Add(n);
            db.userGallery.Add(n);
            db.userGallery.Add(n);
            db.SaveChanges();
            return Ok(true);
        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            ExtendIdentityUser user = userManager.FindByIdAsync("d5aec8fc-1875-4fc8-b499-d6c3d642d929").Result;
            var token = userManager.GeneratePasswordResetTokenAsync(user).Result;
            var x = userManager.ResetPasswordAsync(user, token, "Password").Result;
            return Ok(true);
        }

        [Route("[controller]/[Action]/{r}")]
        [HttpGet]
        public async Task<IActionResult> AddRole(string r)
        {
            ExtendIdentityRole role = new ExtendIdentityRole();
            role.Name = r;
            var x = await roleManager.CreateAsync(role);
            if (x.Succeeded)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }

        [Route("[controller]/[Action]")]
        [HttpGet]
        public IActionResult AddWeekDay()
        {


            DayOfWeek d = new DateTime(2023, 12, 31).DayOfWeek;

            return Ok(d);


        }
    }
}
