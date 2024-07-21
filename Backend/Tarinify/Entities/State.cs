using Trainify.Privilage;

namespace Trainify.Entities
{
    public class State
    {
        public int Id { get; set; }

        public string StateName { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public int CountryId { get; set; }

        public Country country { get; set; }

        public IEnumerable<TrainerInvoice> trainerInvoice { get; set; }

        public IEnumerable<ClientInvoice> ClientInvoice { get; set; }

        public IEnumerable<ExtendIdentityUser> extendIdentityUser { get; set; }
    }
}
