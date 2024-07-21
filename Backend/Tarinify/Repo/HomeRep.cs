using Trainify.Context;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class HomeRep:IHomeRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public HomeRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public HomeClientsCountChartModel GeClientsCountChart(string TrainerId)
        {
            DateTime now = ti.GetCurrentTime();
            DateTime ThirtyDaysAgo = now.AddMonths(-1);

            var ThisMonthMemberships = db.ClientRegisteredPlan.Join(db.trainerPlan, a => a.TrainerPlanId, b => b.Id, (a, b) => new
            {
                TrainerId = b.ExtendIdentityUserId,
                StartDate = a.StartDate
            }).Where(a => a.TrainerId == TrainerId && a.StartDate >= ThirtyDaysAgo && a.StartDate <= now).ToList();


            HomeClientsCountChartModel obj = new HomeClientsCountChartModel();
            obj.data = new List<HomeClientsCountChartDataModel>();
            obj.NewMembersCount = 0;
            for (var dt = ThirtyDaysAgo; dt <= now; dt = dt.AddDays(1))
            {
                var count = ThisMonthMemberships.Where(a => a.StartDate.Date == dt.Date).Count();
                var date = dt.Date;
                if (count == 0)
                {
                    continue;
                }
                HomeClientsCountChartDataModel DataObj = new HomeClientsCountChartDataModel();
                DataObj.Date = date;
                DataObj.Memberships = count;
                obj.data.Add(DataObj);
                obj.NewMembersCount += count;
            }

            //calculate growth
            DateTime begin = now.AddMonths(-2);
            DateTime end = now.AddMonths(-1);
            var LastMonthMemberships = db.ClientRegisteredPlan.Join(db.trainerPlan, a => a.TrainerPlanId, b => b.Id, (a, b) => new
            {
                TrainerId = b.ExtendIdentityUserId,
                StartDate = a.StartDate
            }).Where(a => a.TrainerId == TrainerId && a.StartDate >= begin && a.StartDate <= end).ToList().Count();

            var growth = (((obj.NewMembersCount / LastMonthMemberships) - 1) * 100);
            obj.GrowthPercentage = Math.Round((double)growth, 1);
            return obj;
        }
    }
}
