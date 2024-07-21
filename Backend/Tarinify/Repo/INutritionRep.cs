using Trainify.Entities;
using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface INutritionRep
    {
        Task<bool> AddNutritionPlan(AddNutritionPlanModel obj);

        List<NutritionPlansViewModel> GetTrainerPlans(string UserId);

        List<NutritionPlansViewModel> GetTrainerDraftPlans(string UserId);

        bool DeletePlan(string Token, string TrainerId);

        SingleNutritionPlanViewModel TrainerSingleNutritionPlan(string Token);

        bool EditNutritionPlan(AddNutritionPlanModel obj);

        bool AssignNutritionForClient(AssignNutritionPlanModel obj);

        bool AddCustomFood(AddCustomFoodModel obj);

        List<FoodCategoryViewModel> GetFoodMenu();

        List<GetFoodCategoryViewModel> GetFoodCategories();

        NutritionIdsViewModel GenerateNutrition(GenerateNutritionModel obj);

        Task<bool> DuplicateNutritionPlan(string NutritionToken);
    }
}
