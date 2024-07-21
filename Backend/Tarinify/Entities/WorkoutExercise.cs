namespace Trainify.Entities
{
    public class WorkoutExercise
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public Workout workout { get; set; }

        public int ExerciseId { get; set; }

        public Exercise exersice { get; set; }
    }
}
