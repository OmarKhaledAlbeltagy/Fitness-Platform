namespace Trainify.Entities
{
    public class FoodTags
    {
        public int Id { get; set; }

        public string Tag { get; set; }

        public int FoodId { get; set; }

        public Food food { get; set; }
    }
}
