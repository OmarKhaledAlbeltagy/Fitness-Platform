using Trainify.Privilage;

namespace Trainify.Entities
{
    public class TrainerPlan
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        public int DurationInDays { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }
    }
}
