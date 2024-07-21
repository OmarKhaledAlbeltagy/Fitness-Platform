using Trainify.Privilage;

namespace Trainify.Entities
{
    public class UserGallery
    {
        public int Id { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }

        public byte[]? GalleryImageData { get; set; }

        public string? GalleryImageExtension { get; set; }

        public string? GalleryImageContentType { get; set; }

        public DateTime UploadDateTime { get; set; }

        public string Token { get; set; } = Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
}
