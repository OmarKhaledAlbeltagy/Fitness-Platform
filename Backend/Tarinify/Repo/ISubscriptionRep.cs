using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface ISubscriptionRep
    {
        List<TrainerViewModel> GetAllTrainers();

        List<TrainerPlansViewModel> GetTrainerPlans(string Id);

        bool ClientSubscripePlan(ClientSubscriptionModel obj);
    }
}
