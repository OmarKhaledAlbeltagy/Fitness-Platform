using Trainify.Privilage;

namespace Trainify.Entities
{
    public class UserPost
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public byte[]? ImageData { get; set; }

        public string? ImageExtension { get; set; }

        public string? ImageContentType { get; set; }

        public DateTime dateTime { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }

        public string Token { get; set; } = Guid.NewGuid().ToString().Replace("-", string.Empty);

        public bool IsDeleted { get; set; } = false;
    }
}
