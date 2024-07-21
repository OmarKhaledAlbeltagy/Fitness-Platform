namespace Trainify.Models
{
    public class ResetPasswordModel
    {
        public string UserToken { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
