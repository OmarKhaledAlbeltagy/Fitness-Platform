namespace Trainify.Models
{
    public class SendResetEmailTokenModel
    {
        public string UserToken { get; set; }

        public string NewEmail { get; set; }
    }
}
