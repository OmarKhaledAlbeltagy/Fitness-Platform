namespace Trainify.Models
{
    public class ClientRegisterModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int StateId { get; set; }

        public DateTime Birthday { get; set; }

        public bool Gender { get; set; } // Male = false ----- Female = true

        public int ActivityLevelId { get; set; }

        public int BodyTypeId { get; set; }

        public int WeightGoalId { get; set; }

        public double WeightKG { get; set; } = 0;

        public double WeightLB { get; set; } = 0;

        public double HeightCM { get; set; } = 0;

        public double HeightIN { get; set; } = 0;

        public double HeightFT { get; set; } = 0;

        public string Role { get; } = "Client";
    }
}
