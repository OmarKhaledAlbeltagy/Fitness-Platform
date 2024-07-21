using Trainify.Entities;

namespace Trainify.Models
{
    public class SignupGeneralInfoModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int StateId { get; set; }

        public string RoleId { get; set; }
    }
}
