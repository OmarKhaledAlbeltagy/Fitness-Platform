using Trainify.Privilage;

namespace Trainify.Entities
{
    public class TrainerCertificate
    {
        public int Id { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }

        public string CertificateName { get; set; }
    }
}
