using Trainify.Privilage;

namespace Trainify.Entities
{
    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Calories { get; set; }

        public float Fats { get; set; }

        public float Carb { get; set; }

        public float Protein { get; set; }

        public string ServingSize { get; set; }

        public int FoodCategoryId { get; set; }

        public FoodCategory? foodCategory { get; set; }

        public string Token { get; set; } = Guid.NewGuid().ToString().Replace("-", string.Empty);

        public bool IsDeleted { get; set; } = false;

        public IEnumerable<NutritionPlanMealFood>? nutritionPlanMealFood { get; set; }

        public IEnumerable<FoodTags>? foodTags { get; set; }

        public byte[]? ThumbnailData { get; set; }

        public string? ThumbnailExtension { get; set; }

        public string? ThumbnailContentType { get; set; }
    }
}
