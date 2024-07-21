using Trainify.Privilage;

namespace Trainify.Entities
{
    public class ClientRegisteredPlan
    {
        public int Id { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }

        public int TrainerPlanId { get; set; }

        public TrainerPlan trainerPlan { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ClientInvoice ClientInvoice { get; set; }
    }
}
