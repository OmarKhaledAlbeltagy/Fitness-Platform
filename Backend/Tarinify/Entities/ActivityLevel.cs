namespace Trainify.Entities
{
    public class ActivityLevel
    {
        public int Id { get; set; }

        public string ActivityLevelName { get; set; }

        public float ActivityLevelValue { get; set; }

        public byte[] ThumbnailData { get; set; }

        public string ThumbnailExtension { get; set; }

        public string ThumbnailContentType { get; set; }
    }
}
