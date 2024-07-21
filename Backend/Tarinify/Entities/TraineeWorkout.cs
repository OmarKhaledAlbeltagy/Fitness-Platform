using Trainify.Privilage;

namespace Trainify.Entities
{
    public class ClientWorkout
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public Workout workout { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }
    }
}
