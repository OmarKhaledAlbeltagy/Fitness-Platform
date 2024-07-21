namespace Trainify.Models
{
    public class RegisterModel
    {
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneCodeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int StateId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
