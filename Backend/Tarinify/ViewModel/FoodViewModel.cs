namespace Trainify.ViewModel
{
    public class FoodViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Calories { get; set; }

        public double Fats { get; set; }

        public double Carb { get; set; }

        public double Protein { get; set; }

        public int FoodCategoryId { get; set; }

        public List<string> mealtypes { get; set; }
    }
}
