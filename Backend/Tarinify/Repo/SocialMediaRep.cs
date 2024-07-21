using Trainify.Entities;
using Trainify.Context;
namespace Trainify.Repo
{
    public class SocialMediaRep : ISocialMediaRep
    {
        private readonly DbContainer db;
        public SocialMediaRep(DbContainer db)
        {
            this.db = db;
        }
        public List<SocialMedia> GetAllSocialMedia()
        {
            return db.socialMedia.ToList();
        }
    }
}
