namespace Trainify.Models
{
    public class ContactInfoModel
    {
        public string Id { get; set; }

        public int PhoneCodeId { get; set; }

        public string PhoneNumber { get; set; }

        public List<SocialMediaModel> social { get; set; }
    }
}
