using Trainify.Privilage;

namespace Trainify.Models
{
    public class AddMealModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public string QuantityType { get; set; }

        public int Calories { get; set; }

        public int Fats { get; set; }

        public int Carb { get; set; }

        public int Protein { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public string? Token { get; set; }

        public IFormFile? Thumbnail { get; set; }
    }
}
