namespace Trainify.Models
{
    public class BMIModel
    {
        public bool Gender { get; set; }

        public int Age { get; set; }

        public double WeightLB { get; set; } = 0;

        public double WeightKG { get; set; } = 0;

        public double HeightCM { get; set; } = 0;

        public double HeightFT { get; set; } = 0;

        public double HeightIN { get; set; } = 0;

        public int ActivityLevelId { get; set; }

        public int WeightGoalId { get; set; }
    }
}
