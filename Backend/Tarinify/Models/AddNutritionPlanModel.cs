using System.Text.Json.Serialization;
using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.Models
{
    public class AddNutritionPlanModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public string TrainerId { get; set; }

        public string? ClientId { get; set; }

        public int PlanWeightGoalId { get; set; }

        public string? NutritionPlanToken { get; set; }

        public bool IsDraft { get; set; }

        public List<NutritionPlanMealModel> nutritionPlanMeal { get; set; }
    }
}
