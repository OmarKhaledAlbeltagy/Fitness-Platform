using Trainify.Entities;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface IPropertiesRep
    {
        List<PropertyViewModel> GetAllActivityLevels();

        List<PropertyViewModel> GetAllBodyTypes();

        List<PropertyViewModel> GetAllWeightGoals();

        dynamic GetAllTrainerTitles();
    }
}
