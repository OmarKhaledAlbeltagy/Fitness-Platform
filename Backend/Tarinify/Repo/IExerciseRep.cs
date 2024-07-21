using Trainify.Entities;
using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface IExerciseRep
    {
        bool AddExercise(AddExersiceModel obj);

        bool EditExercise(AddExersiceModel obj);

        List<Exercise> GetMyExercises(string Id);

        bool DeleteExercise(string UserId, string ExerciseToken);

        ExerciseViewModel GetExerciseByToken(string Token);
    }
}
