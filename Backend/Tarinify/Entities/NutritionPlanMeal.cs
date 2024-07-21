using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Trainify.Entities
{
    public class NutritionPlanMeal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NutritionPlanId { get; set; }

        public NutritionPlan nutritionPlan { get; set; }

        public List<NutritionPlanMealFood> nutritionPlanMealFood { get; set; }
    }
}
