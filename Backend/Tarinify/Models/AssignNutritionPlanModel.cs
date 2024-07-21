using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.Models
{
    public class AssignNutritionPlanModel
    {
        public string NutritionPlanToken { get; set; }

        public string ClientId { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }
    }
}
