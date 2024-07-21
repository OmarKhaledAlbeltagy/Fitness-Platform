using Trainify.Privilage;

namespace Trainify.Entities
{
    public class TrainerTitle
    {
        public int Id { get; set; }

        public string Title1 { get; set; }

        public string Title2 { get; set; }

        public IEnumerable<ExtendIdentityUser> extendIdentityUsers { get; set; }
    }
}
