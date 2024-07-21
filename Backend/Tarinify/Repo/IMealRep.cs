using Trainify.Entities;
using Trainify.Models;

namespace Trainify.Repo
{
    public interface IMealRep
    {
        bool AddMeal(AddMealModel obj);

        List<Food> GetMyMeals(string Id);

        bool DeleteMeal(string MealToken);

        Food GetMealByToken(string Token);

        bool EditMeal(AddMealModel obj);
    }
}
