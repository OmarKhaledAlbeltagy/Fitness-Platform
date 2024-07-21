using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface IHomeRep
    {
        HomeClientsCountChartModel GeClientsCountChart(string TrainerId);
    }
}
