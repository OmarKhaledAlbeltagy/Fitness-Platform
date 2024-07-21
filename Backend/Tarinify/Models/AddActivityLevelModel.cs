namespace Trainify.Models
{
    public class AddActivityLevelModel
    {
        public string ActivityLevelName { get; set; }

        public float ActivityLevelValue { get; set; }

        public IFormFile file { get; set; }
    }
}
