namespace Trainify.Models
{
    public class ResetEmailModel
    {
        public string UserToken { get; set; }

        public string NewEmail { get; set; }

        public string Token { get; set; }
    }
}
