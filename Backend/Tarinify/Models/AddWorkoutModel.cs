using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.Models
{
    public class AddWorkoutModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public IFormFile? Thumbnail { get; set; }

        public string UserId { get; set; }

        public List<string>? ExerciseIds { get; set; }

        public string? Token { get; set; }
    }
}
