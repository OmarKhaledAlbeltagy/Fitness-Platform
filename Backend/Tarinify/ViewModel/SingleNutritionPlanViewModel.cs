using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.ViewModel
{
    public class SingleNutritionPlanViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string status { get; set; }

        public string? Description { get; set; }

        public List<NutritionPlanMealViewModel> nutritionPlanMeal { get; set; }

        public string Token { get; set; }

        public DateTime? StartDate { get; set; }

        public NutritionClientViewModel? Client { get; set; }

        public float TotalCalories { get; set; }

        public float Carb { get; set; }

        public float Fat { get; set; }

        public float Protein { get; set; }

        public float CarbPercentage { get; set; }

        public float FatPercentage { get; set; }

        public float ProteinPercentage { get; set; }
    }
}
