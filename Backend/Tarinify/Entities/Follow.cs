using Trainify.Privilage;

namespace Trainify.Entities
{
    public class Follow
    {
        public int Id { get; set; }

        public string FollowerId { get; set; }

        public ExtendIdentityUser Follower { get; set; }

        public string FollowedId { get; set; }

        public ExtendIdentityUser Followed { get; set; }

        public DateTime FollowDateTime { get; set; }
    }
}
