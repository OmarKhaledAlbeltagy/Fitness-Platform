namespace Trainify.Entities
{
    public class FoodCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<Food> food { get; set; }

        public byte[]? ThumbnailData { get; set; }

        public string? ThumbnailExtension { get; set; }

        public string? ThumbnailContentType { get; set; }
    }
}
