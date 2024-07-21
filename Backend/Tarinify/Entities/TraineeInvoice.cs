using Trainify.Privilage;

namespace Trainify.Entities
{
    public class ClientInvoice
    {
        public int Id { get; set; }

        public int InvoiceNumber { get; set; }

        public string FullName { get; set; }

        public int ClientRegisteredPlanId { get; set; }

        public ClientRegisteredPlan ClientRegisteredPlan { get; set; }

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
