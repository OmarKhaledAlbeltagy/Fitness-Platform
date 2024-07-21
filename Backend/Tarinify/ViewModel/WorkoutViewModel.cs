using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.ViewModel
{
    public class WorkoutViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public List<Exercise> exercises { get; set; }

        public List<string> ExercisesTokens { get; set; }

        public string Token { get; set; }
    }
}
