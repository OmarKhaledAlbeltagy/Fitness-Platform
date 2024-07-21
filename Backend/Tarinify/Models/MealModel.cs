namespace Trainify.Models
{
    public class MealModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Carb { get; set; }

        public double Fats { get; set; }

        public int CategoryId { get; set; }

        public int FoodCategoryId { get; set; }

        public int MealTypeId { get; set; }

        public string Tag { get; set; }
    }
}
