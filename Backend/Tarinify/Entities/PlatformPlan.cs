using Trainify.Privilage;

namespace Trainify.Entities
{
    public class PlatformPlan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        public int Months { get; set; }

        public int ClientLimit { get; set; }
    }
}
