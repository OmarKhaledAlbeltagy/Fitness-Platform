namespace Trainify.ViewModel
{
    public class MyProfileViewModel2
    {
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int StateId { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int PhoneCodeId { get; set; }

        public string PhoneCode { get; set; }

        public string? Address { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? PostalCode { get; set; }

        public string? About { get; set; }

        public int? Age { get; set; }

        public DateTime? Birthday { get; set; }

        public string BirthdayString { get; set; }

        public List<LanguageViewModel>? Languages { get; set; }

        public List<ProfileSocialMediaViewModel>? social { get; set; }

        public List<CertificateViewModel> certificates { get; set; }

        public string MinimumBirth { get; set; }

        public string MaximumBirth { get; set; }
    }
}
