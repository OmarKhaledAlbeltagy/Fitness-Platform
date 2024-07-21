using Trainify.Entities;
using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface IWorkoutRep
    {
        bool AddWorkout(AddWorkoutModel obj);

        List<Workout> GetMyWorkouts(string Id);

        bool DeleteWorkout(string UserId, string WorkoutToken);

        WorkoutViewModel GetWorkoutByToken(string Token);

        bool EditWorkout(AddWorkoutModel obj);
    }
}
