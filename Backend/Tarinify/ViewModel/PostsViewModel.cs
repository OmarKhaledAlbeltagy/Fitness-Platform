using Trainify.Privilage;

namespace Trainify.ViewModel
{
    public class PostsViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string DateTime { get; set; }

        public string? Token { get; set; }

        public bool IsImage { get; set; } =  false;
    }
}
