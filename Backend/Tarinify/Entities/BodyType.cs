namespace Trainify.Entities
{
    public class BodyType
    {
        public int Id { get; set; }

        public string BodyTypeName { get; set; }

        public byte[] ThumbnailData { get; set; }

        public string ThumbnailExtension { get; set; }

        public string ThumbnailContentType { get; set; }
    }
}
