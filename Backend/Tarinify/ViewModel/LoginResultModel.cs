namespace Trainify.ViewModel
{
    public class LoginResultModel
    {

        public string result { get; set; } = "Success";

        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public bool IsProfilePicture { get; set; } = false;
    }
}
