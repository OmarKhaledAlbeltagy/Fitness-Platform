using Microsoft.Graph.Models;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class PostRep:IPostRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public PostRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public ReturnPostViewModel AddPost(AddPostModel obj)
        {
            DateTime now = ti.GetCurrentTime();
            UserPost p = new UserPost();
            p.Content = obj.Content;
            p.ExtendIdentityUserId = obj.ExtendIdentityUserId;
            p.dateTime = now;
            if (obj.Image != null)
            {
                var ImgStream = new MemoryStream();
                obj.Image.CopyTo(ImgStream);
                var ImgBytes = ImgStream.ToArray();
                p.ImageData = ImgBytes;
                p.ImageContentType = obj.Image.ContentType;
                p.ImageExtension = obj.Image.FileName.Split('.')[obj.Image.FileName.Split('.').Length - 1];
                ImgStream.Dispose();
            }
            db.post.Add(p);
            db.SaveChanges();

            ReturnPostViewModel res = new ReturnPostViewModel();
            res.Id = p.Id;
            res.Content = p.Content;
            res.DateTime = "Now";

            return res;
        }

        public bool DeleteMyPost(string UserId, string Token)
        {
            UserPost post = db.post.Where(a=>a.Token == Token && a.ExtendIdentityUserId == UserId).FirstOrDefault();
            if (post != null)
            {
                post.IsDeleted = true;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
