using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.ViewModel
{
    public class TrainerPlansViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        public int DurationInDays { get; set; }
    }
}
