using Trainify.Privilage;

namespace Trainify.Entities
{
    public class NutritionPlan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string TrainerId { get; set; }

        public ExtendIdentityUser Trainer { get; set; }

        public string? ClientId { get; set; }

        public ExtendIdentityUser Client { get; set; }

        public IEnumerable<NutritionPlanMeal> nutritionPlanMeal { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string Token { get; set; } = Guid.NewGuid().ToString().Replace("-", string.Empty);

        public bool IsDeleted { get; set; } = false;

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public int WeightGoalId { get; set; }

        public WeightGoal weightGoal { get; set; }

        public bool IsDraft { get; set; } = false;
    }
}
