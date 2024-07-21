using Microsoft.EntityFrameworkCore.Storage;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class WorkoutRep : IWorkoutRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public WorkoutRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public bool AddWorkout(AddWorkoutModel obj)
        {

            DateTime now = ti.GetCurrentTime();

            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Workout res = new Workout();
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

                    db.workout.Add(res);
                    db.SaveChanges();

                    if (obj.ExerciseIds.Count > 0)
                    {
                        var arr = obj.ExerciseIds[0].Split(',');

                        foreach (var item in arr)
                        {
                            WorkoutExercise we = new WorkoutExercise();
                            Exercise ex = db.exersice.Where(a => a.Token == item).First();
                            we.ExerciseId = ex.Id;
                            we.WorkoutId = res.Id;
                            db.workoutExercise.Add(we);
                        }
                        db.SaveChanges();
                    }
                   
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    string m = ex.Message;
                    transaction.Rollback();
                    return false;
                }
               
            }
         
        }

        public bool DeleteWorkout(string UserId, string WorkoutToken)
        {
            Workout w = db.workout.Where(a => a.ExtendIdentityUserId == UserId && a.Token == WorkoutToken).FirstOrDefault();
            if (w != null)
            {
                w.IsDeleted = true;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool EditWorkout(AddWorkoutModel obj)
        {
            DateTime now = ti.GetCurrentTime();

            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Workout res = db.workout.Where(a=>a.Token == obj.Token).FirstOrDefault();
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
           
                    List<WorkoutExercise> wexer = db.workoutExercise.Where(a => a.WorkoutId == res.Id).ToList();
                    db.workoutExercise.RemoveRange(wexer);
                    db.SaveChanges();
                    if (obj.ExerciseIds != null)
                    {
                        var arr = obj.ExerciseIds[0].Split(',');
                        foreach (var item in arr)
                        {
                            WorkoutExercise we = new WorkoutExercise();
                            Exercise ex = db.exersice.Where(a => a.Token == item).First();
                            we.ExerciseId = ex.Id;
                            we.WorkoutId = res.Id;
                            db.workoutExercise.Add(we);
                        }
                        db.SaveChanges();
                    }
                 
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    string m = ex.Message;
                    transaction.Rollback();
                    return false;
                }

            }

        }

        public List<Workout> GetMyWorkouts(string Id)
        {
            List<Workout> res =  db.workout.Where(a => a.ExtendIdentityUserId == Id && a.IsDeleted == false).ToList();
            return res;
        }

        public WorkoutViewModel GetWorkoutByToken(string Token)
        {
            Workout w = db.workout.Where(a => a.Token == Token).FirstOrDefault();

            List<int>? wex = db.workoutExercise.Where(a => a.WorkoutId == w.Id).Select(a=>a.ExerciseId).ToList();
              
            List<Exercise> ex = new List<Exercise>();
           
            WorkoutViewModel res = new WorkoutViewModel();
            res.Id = w.Id;
            res.Name = w.Name;
            res.Description = w.Description;
            res.ExtendIdentityUserId = w.ExtendIdentityUserId;
            res.Created = w.Created;
            res.Modified = w.Modified;
            res.Token = w.Token;

            if (wex != null)
            {
                List<string> extokens = new List<string>();
                foreach (var item in wex)
                {
                    Exercise x = db.exersice.Find(item);
                    ex.Add(x);
                    extokens.Add(x.Token);
                }
                res.ExercisesTokens = extokens;
                res.exercises = ex;
            }


       
            return res;
        }
    }
}
