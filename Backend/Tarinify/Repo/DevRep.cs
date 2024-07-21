using System.Net;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using static Microsoft.Graph.Constants;

namespace Trainify.Repo
{
    public class DevRep:IDevRep
    {
        private readonly DbContainer db;
        private readonly ITimeRep ti;

        public DevRep(DbContainer db, ITimeRep ti)
        {
            this.db = db;
            this.ti = ti;
        }

        public bool AddActivityLevel(AddActivityLevelModel activityLevel)
        {
            ActivityLevel a = new ActivityLevel();
            var ThumbStream = new MemoryStream();
            activityLevel.file.CopyTo(ThumbStream);
            var ThumbBytes = ThumbStream.ToArray();
            a.ThumbnailData = ThumbBytes;
            a.ThumbnailContentType = activityLevel.file.ContentType;
            a.ThumbnailExtension = activityLevel.file.FileName.Split('.')[activityLevel.file.FileName.Split('.').Length - 1];
            a.ActivityLevelValue = activityLevel.ActivityLevelValue;
            a.ActivityLevelName = activityLevel.ActivityLevelName;
            ThumbStream.Dispose();
            db.activityLevel.Add(a);
            db.SaveChanges();
            return true;
        }

        public bool AddBodyType(AddBodyTypeModel bodyType)
        {
            BodyType a = new BodyType();
            var ThumbStream = new MemoryStream();
            bodyType.file.CopyTo(ThumbStream);
            var ThumbBytes = ThumbStream.ToArray();
            a.ThumbnailData = ThumbBytes;
            a.ThumbnailContentType = bodyType.file.ContentType;
            a.ThumbnailExtension = bodyType.file.FileName.Split('.')[bodyType.file.FileName.Split('.').Length - 1];
            a.BodyTypeName = bodyType.BodyTypeName;
            ThumbStream.Dispose();
            db.bodyType.Add(a);
            db.SaveChanges();
            return true;
        }

        public DateTime GetDateTime()
        {
            return ti.GetCurrentTime();
        }

        public string GetRedirectingUrl(string FirstUrl)
        {
            if (string.IsNullOrWhiteSpace(FirstUrl))
                return FirstUrl;

            int maxRedirCount = 8;  // prevent infinite loops
            string SecondUrl = FirstUrl;
            do
            {
                HttpWebRequest req = null;
                HttpWebResponse resp = null;
                try
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(FirstUrl);
                    req.Method = "HEAD";
                    req.AllowAutoRedirect = false;
                    resp = (HttpWebResponse)req.GetResponse();
                    switch (resp.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return SecondUrl;
                        case HttpStatusCode.Redirect:
                        case HttpStatusCode.MovedPermanently:
                        case HttpStatusCode.RedirectKeepVerb:
                        case HttpStatusCode.RedirectMethod:
                            SecondUrl = resp.Headers["Location"];
                            if (SecondUrl == null)
                                return FirstUrl;

                            if (SecondUrl.IndexOf("://", System.StringComparison.Ordinal) == -1)
                            {
                                // Doesn't have a URL Schema, meaning it's a relative or absolute URL
                                Uri u = new Uri(new Uri(FirstUrl), SecondUrl);
                                SecondUrl = u.ToString();
                            }
                            break;
                        default:
                            return SecondUrl;
                    }
                    FirstUrl = SecondUrl;
                }
                catch (WebException)
                {
                    // Return the last known good URL
                    return SecondUrl;
                }
                catch (Exception ex)
                {
                    return "";
                }
                finally
                {
                    if (resp != null)
                        resp.Close();
                }
            } while (maxRedirCount-- > 0);

            return SecondUrl;
        }
    }
}
