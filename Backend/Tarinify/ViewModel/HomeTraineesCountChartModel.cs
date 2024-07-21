namespace Trainify.ViewModel
{
    public class HomeClientsCountChartModel
    {
        public List<HomeClientsCountChartDataModel> data { get; set; }

        public int NewMembersCount { get; set; }

        public double GrowthPercentage { get; set; }
    }
}
