using Trainify.Entities;

namespace Trainify.ViewModel
{
    public class NutritionPlanMealViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float MealCalories { get; set; }

        public List<NutritionPlanMealFoodViewModel> nutritionPlanMealFood { get; set; }
    }
}
