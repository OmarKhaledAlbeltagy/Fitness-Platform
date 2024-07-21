namespace Trainify.Models
{
    public class TrainerRegistrationModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int StateId { get; set; }

        public DateTime Birthday { get; set; }

        public bool Gender { get; set; } // Male = false ----- Female = true

        public int TrainerTitleId { get; set; }

        public List<string>? Certificates { get; set; }

        public IFormFile? profilePicture { get; set; }

        public string Role { get; } = "Trainer";
    }
}
