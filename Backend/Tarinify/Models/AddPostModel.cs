using Trainify.Privilage;

namespace Trainify.Models
{
    public class AddPostModel
    {
        public string Content { get; set; }

        public IFormFile? Image { get; set; }

        public string ExtendIdentityUserId { get; set; }
    }
}
