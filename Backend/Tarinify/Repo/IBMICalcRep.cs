using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface IBMICalcRep
    {
        BMIViewModel CalculateBMI(BMIModel obj);

        List<FoodCategoryViewModel> GetFoodMenu();

        NutritionIdsViewModel GenerateNutrition(GenerateNutritionModel obj);
    }
}
