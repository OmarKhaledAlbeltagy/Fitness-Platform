using Trainify.Privilage;

namespace Trainify.Entities
{
    public class UserLanguage
    {
        public int Id { get; set; }

        public int LanguageId { get; set; }

        public Language language { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }
    }
}
