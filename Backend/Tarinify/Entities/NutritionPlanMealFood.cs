using System.Text.Json.Serialization;

namespace Trainify.Entities
{
    public class NutritionPlanMealFood
    {
        public int Id { get; set; }

        public int FoodId { get; set; }

        public Food food { get; set; }

        public int NutritionPlanMealId { get; set; }

        public NutritionPlanMeal nutritionPlanMeal { get; set; }

        public int Quantity { get; set; }
    }
}
