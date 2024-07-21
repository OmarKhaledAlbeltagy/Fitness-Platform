using Trainify.Privilage;

namespace Trainify.Entities
{
    public class NotCompletedSignup
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int StateId { get; set; }

        public State state { get; set; }

        public string ExtendIdentityRoleId { get; set; }

        public ExtendIdentityRole extendIdentityRole { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
