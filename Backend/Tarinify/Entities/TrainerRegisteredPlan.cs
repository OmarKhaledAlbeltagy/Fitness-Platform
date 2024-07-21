using Trainify.Privilage;

namespace Trainify.Entities
{
    public class TrainerRegisteredPlan
    {
        public int Id { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }

        public int PlatformPlanId { get; set; }

        public PlatformPlan platformPlan { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TrainerInvoice trainerInvoice { get; set; }
    }
}
