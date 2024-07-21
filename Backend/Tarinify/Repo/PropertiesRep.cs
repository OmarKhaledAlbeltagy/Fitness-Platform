using Microsoft.EntityFrameworkCore;
using System.Linq;
using Trainify.Context;
using Trainify.Entities;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class PropertiesRep: IPropertiesRep
    {
        private readonly DbContainer db;

        public PropertiesRep(DbContainer db)
        {
            this.db = db;
        }

        public List<PropertyViewModel> GetAllActivityLevels()
        {
            List<PropertyViewModel> res = db.activityLevel
             .Select(activityLevel => new PropertyViewModel
             {
                 Id = activityLevel.Id,
                 PropertyName = activityLevel.ActivityLevelName
             })
             .ToList();
            return res;
        }

        public List<PropertyViewModel> GetAllBodyTypes()
        {
            List<PropertyViewModel> res = db.bodyType
            .Select(bodyType => new PropertyViewModel
            {
            Id = bodyType.Id,
            PropertyName = bodyType.BodyTypeName
            })
            .ToList();
            return res;
        }

        public dynamic GetAllTrainerTitles()
        {
            var res = db.trainerTitle.Select(item => new
            {
                Id = item.Id,
                Title1 = item.Title1,
                Title2 = item.Title2
            }).ToList();
            return res;
        }

        public List<PropertyViewModel> GetAllWeightGoals()
        {
            List<PropertyViewModel> res = db.weightGoal
            .Select(WeightGoal => new PropertyViewModel
            {
            Id = WeightGoal.Id,
            PropertyName = WeightGoal.GoalName
            })
            .ToList();
            return res;
        }
    }
}
