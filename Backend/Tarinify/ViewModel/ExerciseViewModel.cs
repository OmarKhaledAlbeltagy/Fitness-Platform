using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.ViewModel
{
    public class ExerciseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool IsVideo { get; set; } = true;

        public int? Set { get; set; }

        public int? Rep { get; set; }

        public int? Duration { get; set; }

        public int? Rest { get; set; }

        public string Token { get; set; }

        public int Columns { get; set; }
    }
}
