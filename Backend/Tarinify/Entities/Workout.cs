using Trainify.Privilage;

namespace Trainify.Entities
{
    public class Workout
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public byte[]? ThumbnailData { get; set; }

        public string? ThumbnailExtension { get; set; }

        public string? ThumbnailContentType { get; set; }

        public string ExtendIdentityUserId { get; set; }

        public ExtendIdentityUser extendIdentityUser { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public IEnumerable<WorkoutExercise> workoutExersice { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string Token { get; set; } = Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
}
