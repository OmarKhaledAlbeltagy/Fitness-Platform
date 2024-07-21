using Microsoft.AspNetCore.Identity;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.Privilage;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class SubscriptionRep : ISubscriptionRep
    {
        private readonly DbContainer db;
        private readonly UserManager<ExtendIdentityUser> userManager;
        private readonly ITimeRep ti;

        public SubscriptionRep(DbContainer db, UserManager<ExtendIdentityUser> userManager, ITimeRep ti)
        {
            this.db = db;
            this.userManager = userManager;
            this.ti = ti;
        }

        public List<TrainerViewModel> GetAllTrainers()
        {
            List<TrainerViewModel> res = userManager.GetUsersInRoleAsync("Trainer").Result.Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            {
                Id = a.Id,
                TrainerName = a.FirstName + " " + a.LastName,
                StateName = b.StateName,
                CountryId = b.CountryId,
                MobileNumber = a.PhoneNumber,
                Email = a.Email
            }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new TrainerViewModel
            {
                Id = a.Id,
                TrainerName = a.TrainerName,
                StateName = a.StateName,
                CountryName = b.CountryName,
                Email = a.Email,
                MobileNumber = b.PhoneCode+a.MobileNumber
            }).ToList();

            return res;

        }

        public List<TrainerPlansViewModel> GetTrainerPlans(string Id)
        {
            List<TrainerPlan> plans = db.trainerPlan.Where(a=>a.ExtendIdentityUserId ==  Id).ToList();
                
      

            List<TrainerPlansViewModel> res = new List<TrainerPlansViewModel>();
            foreach (var item in res)
            {
                TrainerPlansViewModel obj = new TrainerPlansViewModel();
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.Description = item.Description;
                obj.DurationInDays = item.DurationInDays;
                res.Add(obj);
            }
            return res;
        }

        public bool ClientSubscripePlan(ClientSubscriptionModel obj)
        {
            throw new NotImplementedException();
            //DateTime now = ti.GetCurrentTime();
            //TrainerPlan Plan = db.trainerPlan.Where(a => a.Id == obj.PlanId && a.ExtendIdentityUserId == obj.TrainerId).First();
            //TrainersPlansDuration PlanDuration = db.trainersPlansDuration.Find(Plan.TrainersPlansDurationId);
            //ClientRegisteredPlan res = new ClientRegisteredPlan();
            //res.StartDate = now;
            //res.EndDate = now.AddMonths(PlanDuration.Months);
            //res.TrainerPlanId = Plan.Id;
            //res.ExtendIdentityUserId = obj.ClientId;
            //db.ClientRegisteredPlan.Add(res);
            //db.SaveChanges();
            //return true;
        }
    }
}
