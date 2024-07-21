using Trainify.Privilage;

namespace Trainify.Entities
{
    public class TrainerInvoice
    {
        public int Id { get; set; }

        public int InvoiceNumber { get; set; }

        public string FullName { get; set; }

        public int TrainerRegisteredPlanId { get; set; }

        public TrainerRegisteredPlan trainerRegisteredPlan { get; set; }

        public string? Address { get; set; }

        public int? StateId { get; set; }

        public State state { get; set; }

        public string? PostalCode { get; set; }

        public DateTime Issued { get; set; }

        public int TotalPrice { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
