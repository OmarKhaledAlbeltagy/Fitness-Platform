using Trainify.Entities;
using Trainify.Privilage;

namespace Trainify.Models
{
    public class AddExersiceModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public IFormFile? Thumbnail { get; set; }

        public IFormFile? Example { get; set; }

        public string UserId { get; set; }

        public int? Set { get; set; }

        public int? Rep { get; set; }

        public int? Duration { get; set; }

        public int? Rest { get; set; }

        public string? Token { get; set; }
    }
}
