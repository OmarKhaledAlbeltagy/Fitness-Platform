using Trainify.Context;
using Trainify.Models;
using Trainify.Entities;
using Microsoft.Graph.Models;
using System.Runtime.CompilerServices;

namespace Trainify.Repo
{
    public class MealRep : IMealRep
    {
        private readonly DbContainer db;

        public MealRep(DbContainer db)
        {
            this.db = db;
        }

        public bool AddMeal(AddMealModel obj)
        {
            Food res = new Food();
            res.Name = obj.Name;
            res.Calories = obj.Calories;
            res.Protein = obj.Protein;
            res.Carb = obj.Carb;
            res.Fats = obj.Fats;
            if (obj.Thumbnail != null && obj.Thumbnail.Length > 0)
            {
                var ThumbStream = new MemoryStream();
                obj.Thumbnail.CopyTo(ThumbStream);
                var ThumbBytes = ThumbStream.ToArray();
                res.ThumbnailData = ThumbBytes;
                res.ThumbnailContentType = obj.Thumbnail.ContentType;
                res.ThumbnailExtension = obj.Thumbnail.FileName.Split('.')[obj.Thumbnail.FileName.Split('.').Length - 1];
                ThumbStream.Dispose();
            }

            db.food.Add(res);
            db.SaveChanges();
            return true;
        }

        public bool DeleteMeal(string MealToken)
        {
            Food res = db.food.Where(a=>a.Token == MealToken).FirstOrDefault();
            if (res != null)
            {
                res.IsDeleted = true;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EditMeal(AddMealModel obj)
        {
            Food res = db.food.Where(a => a.Token == obj.Token).First();
            res.Name = obj.Name;
            res.Calories = obj.Calories;
            res.Protein = obj.Protein;
            res.Carb = obj.Carb;
            res.Fats = obj.Fats;
            if (obj.Thumbnail != null && obj.Thumbnail.Length > 0)
            {
                var ThumbStream = new MemoryStream();
                obj.Thumbnail.CopyTo(ThumbStream);
                var ThumbBytes = ThumbStream.ToArray();
                res.ThumbnailData = ThumbBytes;
                res.ThumbnailContentType = obj.Thumbnail.ContentType;
                res.ThumbnailExtension = obj.Thumbnail.FileName.Split('.')[obj.Thumbnail.FileName.Split('.').Length - 1];
                ThumbStream.Dispose();
            }
            db.SaveChanges();
            return true;
        }

        public Food GetMealByToken(string Token)
        {
            Food res = db.food.Where(a=>a.Token == Token).FirstOrDefault();
            return res;
        }

        public List<Food> GetMyMeals(string Id)
        {
            List<Food> res = db.food.Where(a=>a.IsDeleted == false).OrderBy(a=>a.Name).ToList();
            return res;
        }
    }
}
