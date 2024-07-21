namespace Trainify.Models
{
    public class DeleteAddFoodTagsModel
    {
        public string Name { get; set; }

        public float Calories { get; set; }

        public float Protein { get; set; }

        public float Carb { get; set; }

        public float Fats { get; set; }

        public string Tag { get; set; }

        public int CategoryId { get; set; }
    }
}
