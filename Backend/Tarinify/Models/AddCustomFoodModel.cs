using Trainify.Entities;

namespace Trainify.Models
{
    public class AddCustomFoodModel
    {
        public string Name { get; set; }

        public float Calories { get; set; }

        public float Fats { get; set; }

        public float Carb { get; set; }

        public float Protein { get; set; }

        public int FoodCategoryId { get; set; }

        public IFormFile Image { get; set; }

        public string ServingSize { get; set; }
    }
}
