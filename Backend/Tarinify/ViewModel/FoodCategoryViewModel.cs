using Trainify.Entities;

namespace Trainify.ViewModel
{
    public class FoodCategoryViewModel
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public List<FoodViewModel> food { get; set; }
    }
}
