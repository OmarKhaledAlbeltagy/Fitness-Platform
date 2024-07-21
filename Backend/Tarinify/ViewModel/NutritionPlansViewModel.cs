using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.ViewModel
{
    public class NutritionPlansViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string Token { get; set; }

        public double TotalCalories { get; set; }

        public string? ClientName { get; set; }

        public string PlanGoal { get; set; }

        public string status { get; set; }

        public bool IsDraft { get; set; }
    }
}
