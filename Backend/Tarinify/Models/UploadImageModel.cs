namespace Trainify.Models
{
    public class UploadImageModel
    {
        public string UserId { get; set; }

        public IFormFile Picture { get; set; }
    }
}
