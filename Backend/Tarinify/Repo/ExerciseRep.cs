using System.Diagnostics;
using Trainify.Context;
using Trainify.Models;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using System.IO;
using Trainify.Entities;
using Microsoft.Graph.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class ExerciseRep : IExerciseRep
    {
        private readonly DbContainer db;
        private readonly IDevFuncRep dev;
        private readonly ITimeRep ti;
        public ExerciseRep(DbContainer db, IDevFuncRep dev, ITimeRep ti)
        {
            this.db = db;
            this.dev = dev;
            this.ti = ti;
        }

        public bool AddExercise(AddExersiceModel obj)
        {
            bool IsVideo = false;
            if (obj.Example.Length > 0 && obj.Example != null)
            {
                IsVideo = dev.IsVideo(obj.Example);
            }

            DateTime now = ti.GetCurrentTime();

            Exercise res = new Exercise();
            res.Set = obj.Set;
            res.Rep = obj.Rep;
            res.Duration = obj.Duration;
            res.Rest = obj.Rest;
            res.Name = obj.Name;
            res.Description = obj.Description;
            res.Created = now;
            res.Modified = now;
            res.ExtendIdentityUserId = obj.UserId;
            if (obj.Thumbnail != null && obj.Thumbnail.Length > 0)
            {
                var ThumbStream = new MemoryStream();
                obj.Thumbnail.CopyTo(ThumbStream);
                var ThumbBytes = ThumbStream.ToArray();
                res.ThumbnailData = ThumbBytes;
                res.ThumbnailContentType = obj.Thumbnail.ContentType;
                res.ThumbnailExtension = obj.Thumbnail.FileName.Split('.')[obj.Thumbnail.FileName.Split('.').Length - 1];
                ThumbStream.Dispose();
            }
            if (obj.Example.FileName != "no" && obj.Example.Length > 0 && obj.Example != null && IsVideo)
            {
                var ExampleStream = new MemoryStream();
                int x = ExampleStream.Capacity;
                obj.Example.CopyTo(ExampleStream);
                ExampleStream.Position = 0;
                byte[] ExampleBytes = ExampleStream.ToArray();
                ExampleStream.Position = 0;
                res.ExampleData = ExampleBytes;
                res.ExampleContentType = obj.Example.ContentType;
                res.ExampleExtension = obj.Example.FileName.Split('.')[obj.Example.FileName.Split('.').Length - 1];
                ExampleStream.Dispose();
            }

            db.exersice.Add(res);
            db.SaveChanges();
            return true;
        }

        public bool DeleteExercise(string UserId, string ExerciseToken)
        {
            Exercise ex = db.exersice.Where(a => a.ExtendIdentityUserId == UserId && a.Token == ExerciseToken).FirstOrDefault();
            if (ex != null)
            {
                ex.IsDeleted = true;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool EditExercise(AddExersiceModel obj)
        {
            bool IsVideo = false;
            if (obj.Example.Length > 0 && obj.Example != null)
            {
                IsVideo = dev.IsVideo(obj.Example);
            }

            DateTime now = ti.GetCurrentTime();
            Exercise res = db.exersice.Where(a=>a.Token == obj.Token).FirstOrDefault();
            res.Set = obj.Set;
            res.Rep = obj.Rep;
            res.Duration = obj.Duration;
            res.Rest = obj.Rest;
            res.Name = obj.Name;
            res.Description = obj.Description;
            res.Modified = now;
            if (obj.Thumbnail != null && obj.Thumbnail.Length > 0)
            {
                var ThumbStream = new MemoryStream();
                obj.Thumbnail.CopyTo(ThumbStream);
                var ThumbBytes = ThumbStream.ToArray();
                res.ThumbnailData = ThumbBytes;
                res.ThumbnailContentType = obj.Thumbnail.ContentType;
                res.ThumbnailExtension = obj.Thumbnail.FileName.Split('.')[obj.Thumbnail.FileName.Split('.').Length - 1];
                ThumbStream.Dispose();
            }
            if (obj.Example.FileName != "no" && obj.Example.Length > 0 && obj.Example != null && IsVideo)
            {
                var ExampleStream = new MemoryStream();
                int x = ExampleStream.Capacity;
                obj.Example.CopyTo(ExampleStream);
                ExampleStream.Position = 0;
                byte[] ExampleBytes = ExampleStream.ToArray();
                ExampleStream.Position = 0;
                res.ExampleData = ExampleBytes;
                res.ExampleContentType = obj.Example.ContentType;
                res.ExampleExtension = obj.Example.FileName.Split('.')[obj.Example.FileName.Split('.').Length - 1];
                ExampleStream.Dispose();
            }
            db.SaveChanges();
            return true;
        }

        public ExerciseViewModel GetExerciseByToken(string Token)
        {
            Exercise ex = db.exersice.Where(a=>a.Token == Token).FirstOrDefault();
            ExerciseViewModel res = new ExerciseViewModel();
            res.Set = ex.Set;
            res.Token = Token;
            res.Rep = ex.Rep;
            res.Rest = ex.Rest;
            res.Duration = ex.Duration;
            res.Id = ex.Id;
            res.Token = ex.Token;
            res.IsVideo = ex.ExampleData == null || ex.ExampleContentType == "" || ex.ExampleContentType == null ? false : true;
            res.Description = ex.Description;
            res.Name = ex.Name;
            res.Columns = 0;
            if (ex.Rep != null && ex.Rep != 0)
            {
                res.Columns = res.Columns + 1;
            }
            if (ex.Set != null && ex.Set != 0)
            {
                res.Columns = res.Columns + 1;
            }
            if (ex.Rest != null && ex.Rest != 0)
            {
                res.Columns = res.Columns + 1;
            }
            if (ex.Duration != null && ex.Duration != 0)
            {
                res.Columns = res.Columns + 1;
            }
            return res;
        }

        public List<Exercise> GetMyExercises(string Id)
        {
            List<Exercise> res = db.exersice.Where(a => a.ExtendIdentityUserId == Id && a.IsDeleted == false).OrderByDescending(a=>a.Created).ToList();
            return res;
        }
    }
}
