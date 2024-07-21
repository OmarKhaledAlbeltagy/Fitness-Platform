namespace Trainify.Models
{
    public class ResetForgottenPasswordModel
    {
        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string Token { get; set; }
    }
}
