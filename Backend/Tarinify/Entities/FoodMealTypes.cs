namespace Trainify.Entities
{
    public class FoodMealTypes
    {
        public int Id { get; set; }

        public int FoodId { get; set; }

        public Food food { get; set; }

        public int MealTypesId { get; set; }

        public MealTypes mealTypes { get; set; }
    }
}
