using Trainify.Privilage;

namespace Trainify.Entities
{
    public class UserSocialMedia
    {
        public int Id { get; set; }

        public int SocialMediaId { get; set; }

        public SocialMedia socialMedia { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }

        public string Url { get; set; }
    }
}
