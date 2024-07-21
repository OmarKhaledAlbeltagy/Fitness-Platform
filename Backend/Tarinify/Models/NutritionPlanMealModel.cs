using System.Text.Json.Serialization;
using Trainify.Entities;

namespace Trainify.Models
{
    public class NutritionPlanMealModel
    {
        public string Name { get; set; }


        public List<NutritionPlanMealFoodModel> nutritionPlanMealFood { get; set; }
    }
}
