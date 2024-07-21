using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Functions.Now;
using Microsoft.Graph.Models;
using System.Net.Mail;
using System.Net;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.Privilage;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class MyProfileRep : IMyProfileRep
    {
        private readonly DbContainer db;
        private readonly UserManager<ExtendIdentityUser> userManager;
        private readonly ITimeRep ti;
        public MyProfileRep(DbContainer db, UserManager<ExtendIdentityUser> userManager, ITimeRep ti)
        {
            this.db = db;
            this.userManager = userManager;
            this.ti = ti;
        }

        public bool ChangeCover(UploadImageModel obj)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(obj.UserId).Result;
            var PicStream = new MemoryStream();
            obj.Picture.CopyTo(PicStream);
            var PicBytes = PicStream.ToArray();
            user.CoverData = PicBytes;
            user.CoverContentType = obj.Picture.ContentType;
            user.CoverExtension = obj.Picture.FileName.Split('.')[obj.Picture.FileName.Split('.').Length - 1];
            PicStream.Dispose();
            db.SaveChanges();
            return true;
        }

        public bool ChangeProfilePicture(UploadImageModel obj)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(obj.UserId).Result;
            var PicStream = new MemoryStream();
            obj.Picture.CopyTo(PicStream);
            var PicBytes = PicStream.ToArray();
            user.ProfilePictureData = PicBytes;
            user.ProfilePictureContentType = obj.Picture.ContentType;
            user.ProfilePictureExtension = obj.Picture.FileName.Split('.')[obj.Picture.FileName.Split('.').Length - 1];
            PicStream.Dispose();
            db.SaveChanges();
            return true;
        }

        public List<PostsViewModel> GetMyPosts(string Id)
        {
            DateTime now = ti.GetCurrentTime();
            List<PostsViewModel> res = new List<PostsViewModel>();
            List<UserPost> posts = db.post.Where(a => a.ExtendIdentityUserId == Id && a.IsDeleted == false).OrderByDescending(a => a.dateTime).ToList();
            foreach (var post in posts)
            {
                PostsViewModel p = new PostsViewModel();
                p.Id = post.Id;
                p.Content = post.Content;

                if (post.ImageData != null)
                {
                    p.IsImage = true;
                }

                if ((int)(((TimeSpan)(now - post.dateTime)).TotalDays) <= 1)
                {
                    p.DateTime = "Today " + post.dateTime.ToString("hh:mm tt");
                }
                else
                {
                    if ((int)(((TimeSpan)(now - post.dateTime)).TotalDays) < 2)
                    {
                        p.DateTime = "Yesterday " + post.dateTime.ToString("hh:mm tt");
                    }

                    else
                    {

                        if ((int)(((TimeSpan)(now - post.dateTime)).TotalDays) < 7)
                        {
                            p.DateTime = post.dateTime.ToString("dddd hh:mm tt");
                        }
                        else
                        {
                            p.DateTime = post.dateTime.ToString("dd MMMM yyyy, hh:mm tt");
                        }
                    }
                }



                p.Token = post.Token;
                res.Add(p);
            }
            return res;
        }

        public MyProfileViewModel GetMyProfile(string Id)
        {
            //try
            //{
            //    DateTime now = ti.GetCurrentTime();
            //    var x = userManager.Users.Where(a => a.Id == Id).Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            //    {
            //        Id = a.Id,
            //        FirstName = a.FirstName,
            //        LastName = a.LastName,
            //        PhoneNumber = a.PhoneNumber,
            //        Email = a.Email,
            //        StateId = a.StateId,
            //        PhoneCodeId = a.PhoneCodeId,
            //        CountryId = b.CountryId,
            //        Address = a.Address,
            //        ShortBio = a.ShortBio,
            //        RegistrationDateTime = a.RegistrationDateTime,
            //        PostalCode = a.PostalCode,
            //        About = a.About,
            //        Birthday = a.Birthday
            //    }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new
            //    {
            //        Id = a.Id,
            //        FirstName = a.FirstName,
            //        LastName = a.LastName,
            //        PhoneNumber = a.PhoneNumber,
            //        Email = a.Email,
            //        StateId = a.StateId,
            //        PhoneCodeId = a.PhoneCodeId,
            //        CountryId = a.CountryId,
            //        ContryName = b.CountryName,
            //        PhoneCode = b.PhoneCode,
            //        Address = a.Address,
            //        ShortBio = a.ShortBio,
            //        RegistrationDateTime = a.RegistrationDateTime,
            //        PostalCode = a.PostalCode,
            //        About = a.About,
            //        Birthday = a.Birthday
            //    }).First();

            //    MyProfileViewModel res = new MyProfileViewModel();
            //    res.FirstName = x.FirstName;
            //    res.LastName = x.LastName;
            //    res.PhoneNumber = x.PhoneNumber;
            //    res.Email = x.Email;
            //    res.StateId = x.StateId;
            //    res.PhoneCodeId = x.PhoneCodeId;
            //    res.PhoneCode = x.PhoneCode;
            //    res.CountryId = x.CountryId;
            //    res.CountryName = x.ContryName;
            //    res.Address = x.Address;
            //    res.RegistrationDateTime = x.RegistrationDateTime;
            //    res.PostalCode = x.PostalCode;
            //    res.About = x.About;
            //    res.ShortBio = x.ShortBio;
                
            //    if (x.Birthday != null)
            //    {
            //        res.Birthday = ((DateTime)x.Birthday).Date;
            //        res.BirthdayString = ((DateTime)x.Birthday).ToString("yyyy-MM-dd");
            //        res.Age = (int)(((TimeSpan)(now - x.Birthday)).TotalDays / 365.2425);
            //    }

            //    res.Followers = db.follow.Where(a => a.FollowedId == Id).Count();
            //    res.MinimumBirth = now.AddYears(-100).ToString("yyyy-MM-dd");
            //    res.MaximumBirth = now.AddYears(-5).ToString("yyyy-MM-dd");
            //    List<UserLanguage> userLang = db.userLanguage.Where(a => a.ExtendIdentityUserId == Id).ToList();
            //    res.Languages = new List<LanguageViewModel>();

            //    foreach (var lang in userLang)
            //    {
            //        Language langg = db.language.Find(lang.LanguageId);
            //        LanguageViewModel lvm = new LanguageViewModel();
            //        lvm.Id = langg.Id;
            //        lvm.LanguageName = langg.LanguageName;
            //        res.Languages.Add(lvm);
            //    }

            //    List<SocialMedia> social = db.socialMedia.ToList();
            //    List<UserSocialMedia> usm = db.userSocialMedia.Where(a => a.ExtendIdentityUserId == Id).ToList();
            //    List<ProfileSocialMediaViewModel> psm = new List<ProfileSocialMediaViewModel>();

            //    foreach (var s in usm)
            //    {
            //        ProfileSocialMediaViewModel ps = new ProfileSocialMediaViewModel();
            //        ps.Id = s.Id;
            //        ps.Name = social.Where(a=>a.Id == s.SocialMediaId).First().Name;
            //        ps.SocialMediaId = s.SocialMediaId;
            //        ps.Icon = social.Where(a => a.Id == s.SocialMediaId).First().Icon;
            //        ps.Url = s.Url;
            //        psm.Add(ps);
            //    }

            //    res.social = psm;


            //    List<UserGallery> gallery = db.userGallery.Where(a=>a.ExtendIdentityUserId == Id).ToList();

            //    res.GalleryTokens = new List<string>();

            //    foreach (var item in gallery)
            //    {
            //        res.GalleryTokens.Add(item.Token);
            //    }
            //    List<UserPost> posts = db.post.Where(a => a.ExtendIdentityUserId == Id && a.IsDeleted == false).OrderByDescending(a=>a.dateTime).ToList();
            //    res.posts = new List<PostsViewModel>();
            //    foreach (var post in posts)
            //    {
            //        PostsViewModel p = new PostsViewModel();
            //        p.Id = post.Id;
            //        p.Content = post.Content;

            //        if (post.ImageData != null)
            //        {
            //            p.IsImage = true;
            //        }

            //        if ((int)(((TimeSpan)(now - post.dateTime)).TotalDays) <= 1)
            //        {
            //            p.DateTime = "Today "+ post.dateTime.ToString("hh:mm tt");
            //        }
            //        else
            //        {
            //            if ((int)(((TimeSpan)(now - post.dateTime)).TotalDays) < 2)
            //            {
            //                p.DateTime = "Yesterday "+post.dateTime.ToString("hh:mm tt");
            //            }

            //            else
            //            {

            //                if ((int)(((TimeSpan)(now - post.dateTime)).TotalDays) < 7)
            //                {
            //                    p.DateTime = post.dateTime.ToString("dddd");
            //                }
            //                else
            //                {
            //                    p.DateTime = post.dateTime.ToString("dd MMMM yyyy, hh:mm tt");
            //                }
            //            }
            //        }


                
            //        p.Token = post.Token;
            //        res.posts.Add(p);
            //    }


            //    res.certificates = new List<CertificateViewModel>();

            //    List<TrainerCertificate> tc = db.trainerCertificate.Where(a => a.ExtendIdentityUserId == Id).ToList();

            //    foreach (var item in tc)
            //    {
            //        CertificateViewModel cvm = new CertificateViewModel();
            //        cvm.Certificate = item.CertificateName;
            //        cvm.Id = item.Id;
            //        res.certificates.Add(cvm);
            //    }

            //    return res;

            //}
            //catch (Exception ex)
            //{
            //    var x = ex.Message;
            //    throw;
            //}
            throw new NotImplementedException();
        }

        public MyProfileViewModel1 GetMyProfile1(string Id)
        {
            //try
            //{
            //    DateTime now = ti.GetCurrentTime();
            //    ExtendIdentityUser x = userManager.FindByIdAsync(Id).Result;

            //    MyProfileViewModel1 res = new MyProfileViewModel1();
            //    res.ShortBio = x.ShortBio;
            //    res.Followers = db.follow.Where(a => a.FollowedId == Id).Count();
            //    List<UserGallery> gallery = db.userGallery.Where(a => a.ExtendIdentityUserId == Id).ToList();
            //    res.GalleryTokens = new List<string>();
            //    foreach (var item in gallery)
            //    {
            //        res.GalleryTokens.Add(item.Token);
            //    }
            

            //    return res;

            //}
            //catch (Exception ex)
            //{
            //    var x = ex.Message;
            //    throw;
            //}

            throw new NotImplementedException();
        }

        public MyProfileViewModel2 GetMyProfile2(string Id)
        {
            //try
            //{
            //    DateTime now = ti.GetCurrentTime();
            //    var x = userManager.Users.Where(a => a.Id == Id).Join(db.state, a => a.StateId, b => b.Id, (a, b) => new
            //    {
            //        Id = a.Id,
            //        FirstName = a.FirstName,
            //        LastName = a.LastName,
            //        PhoneNumber = a.PhoneNumber,
            //        Email = a.Email,
            //        StateId = a.StateId,
            //        PhoneCodeId = a.PhoneCodeId,
            //        CountryId = b.CountryId,
            //        Address = a.Address,
            //        RegistrationDateTime = a.RegistrationDateTime,
            //        PostalCode = a.PostalCode,
            //        About = a.About,
            //        Birthday = a.Birthday
            //    }).Join(db.country, a => a.CountryId, b => b.Id, (a, b) => new
            //    {
            //        Id = a.Id,
            //        FirstName = a.FirstName,
            //        LastName = a.LastName,
            //        PhoneNumber = a.PhoneNumber,
            //        Email = a.Email,
            //        StateId = a.StateId,
            //        PhoneCodeId = a.PhoneCodeId,
            //        CountryId = a.CountryId,
            //        ContryName = b.CountryName,
            //        PhoneCode = b.PhoneCode,
            //        Address = a.Address,
            //        RegistrationDateTime = a.RegistrationDateTime,
            //        PostalCode = a.PostalCode,
            //        About = a.About,
            //        Birthday = a.Birthday
            //    }).First();

            //    MyProfileViewModel2 res = new MyProfileViewModel2();
            //    res.FirstName = x.FirstName;
            //    res.LastName = x.LastName;
            //    res.PhoneNumber = x.PhoneNumber;
            //    res.Email = x.Email;
            //    res.StateId = x.StateId;
            //    res.PhoneCodeId = x.PhoneCodeId;
            //    res.PhoneCode = x.PhoneCode;
            //    res.CountryId = x.CountryId;
            //    res.CountryName = x.ContryName;
            //    res.Address = x.Address;
            //    res.RegistrationDateTime = x.RegistrationDateTime;
            //    res.PostalCode = x.PostalCode;
            //    res.About = x.About;

            //    if (x.Birthday != null)
            //    {
            //        res.Birthday = ((DateTime)x.Birthday).Date;
            //        res.BirthdayString = ((DateTime)x.Birthday).ToString("yyyy-MM-dd");
            //        res.Age = (int)(((TimeSpan)(now - x.Birthday)).TotalDays / 365.2425);
            //    }

            //    res.MinimumBirth = now.AddYears(-100).ToString("yyyy-MM-dd");
            //    res.MaximumBirth = now.AddYears(-5).ToString("yyyy-MM-dd");
            //    List<UserLanguage> userLang = db.userLanguage.Where(a => a.ExtendIdentityUserId == Id).ToList();
            //    res.Languages = new List<LanguageViewModel>();

            //    foreach (var lang in userLang)
            //    {
            //        Language langg = db.language.Find(lang.LanguageId);
            //        LanguageViewModel lvm = new LanguageViewModel();
            //        lvm.Id = langg.Id;
            //        lvm.LanguageName = langg.LanguageName;
            //        res.Languages.Add(lvm);
            //    }

            //    List<SocialMedia> social = db.socialMedia.ToList();
            //    List<UserSocialMedia> usm = db.userSocialMedia.Where(a => a.ExtendIdentityUserId == Id).ToList();
            //    List<ProfileSocialMediaViewModel> psm = new List<ProfileSocialMediaViewModel>();

            //    foreach (var s in usm)
            //    {
            //        ProfileSocialMediaViewModel ps = new ProfileSocialMediaViewModel();
            //        ps.Id = s.Id;
            //        ps.Name = social.Where(a => a.Id == s.SocialMediaId).First().Name;
            //        ps.SocialMediaId = s.SocialMediaId;
            //        ps.Icon = social.Where(a => a.Id == s.SocialMediaId).First().Icon;
            //        ps.Url = s.Url;
            //        psm.Add(ps);
            //    }

            //    res.social = psm;

            //    res.certificates = new List<CertificateViewModel>();

            //    List<TrainerCertificate> tc = db.trainerCertificate.Where(a => a.ExtendIdentityUserId == Id).ToList();

            //    foreach (var item in tc)
            //    {
            //        CertificateViewModel cvm = new CertificateViewModel();
            //        cvm.Certificate = item.CertificateName;
            //        cvm.Id = item.Id;
            //        res.certificates.Add(cvm);
            //    }

            //    return res;

            //}
            //catch (Exception ex)
            //{
            //    var x = ex.Message;
            //    throw;
            //}
            throw new NotImplementedException();
        }

        public List<ProfileSocialMediaViewModel> GetMySocialMedia(string Id)
        {
            List<ProfileSocialMediaViewModel> res = new List<ProfileSocialMediaViewModel>();
            List<SocialMedia> social = db.socialMedia.ToList();
            List<UserSocialMedia> usersocial = db.userSocialMedia.Where(a => a.ExtendIdentityUserId == Id).ToList();

            foreach (var s in usersocial)
            {
                ProfileSocialMediaViewModel ps = new ProfileSocialMediaViewModel();
                ps.Id = s.Id;
                ps.Name = social.Where(a => a.Id == s.SocialMediaId).First().Name;
                ps.SocialMediaId = s.SocialMediaId;
                ps.Icon = social.Where(a => a.Id == s.SocialMediaId).First().Icon;
                ps.Url = s.Url;
                res.Add(ps);
            }

            return res;
        }

        public bool UpdateAccountSetting(AccountSettingModel obj)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(obj.Id).Result;
            user.FirstName = obj.FirstName;
            user.LastName = obj.LastName;
            user.Birthday = (DateTime)obj.Birthday;
            user.ShortBio = obj.ShortBio;
            user.About = obj.About;
            //userManager.SetEmailAsync(user, obj.Email);
            //userManager.SetUserNameAsync(user, obj.Email);
            db.SaveChanges();
            if (user.Email != obj.Email || user.UserName != obj.Email)
            {
                string token = userManager.GenerateChangeEmailTokenAsync(user, obj.Email).Result;
                string body = token;
                MailMessage m = new MailMessage();
                m.To.Add(obj.Email);
                m.Subject = "Trainify Email Confirmation";
                m.From = new MailAddress("example@abc.com");
                m.Sender = new MailAddress("example@abc.com");
                m.IsBodyHtml = true;
                m.Body = body;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("SMTP-Server", 587);
                smtp.EnableSsl = false;
                smtp.Credentials = new NetworkCredential("example@abc.com", "Password");
                smtp.Send(m);
            }
            return true;
        }

        public bool UpdateBio(BioModel obj)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(obj.Id).Result;
            user.About = obj.Bio;
            user.ShortBio = obj.ShortBio;
            db.SaveChanges();
            return true;
        }

        public bool UpdateContactInfo(ContactInfoModel obj)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    ExtendIdentityUser user = userManager.FindByIdAsync(obj.Id).Result;
                    var x = userManager.SetPhoneNumberAsync(user, obj.PhoneNumber).Result;
                    user.PhoneCodeId = obj.PhoneCodeId;
                    List<UserSocialMedia> oldusersocial = db.userSocialMedia.Where(a => a.ExtendIdentityUserId == obj.Id).ToList();
                    db.userSocialMedia.RemoveRange(oldusersocial);
                    db.SaveChanges();
                    foreach (var item in obj.social)
                    {
                        UserSocialMedia userSocial = new UserSocialMedia();
                        userSocial.ExtendIdentityUserId = obj.Id;
                        userSocial.SocialMediaId = item.SocialMediaId;
                        userSocial.Url = item.Url;
                        db.userSocialMedia.Add(userSocial);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateLanguages(LanguagesModel obj)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    ExtendIdentityUser user =  userManager.FindByIdAsync(obj.Id).Result;
                    List<UserLanguage> ul = db.userLanguage.Where(a => a.ExtendIdentityUserId == obj.Id).ToList();
                    db.userLanguage.RemoveRange(ul);
                    db.SaveChanges();

                    foreach (var item in obj.Languages)
                    {
                        UserLanguage ulobj = new UserLanguage();
                        ulobj.ExtendIdentityUserId = obj.Id;
                        ulobj.LanguageId = item;
                        db.userLanguage.Add(ulobj);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateLocationInfo(LocationInfoModel obj)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(obj.Id).Result;
            user.StateId = obj.StateId;
            user.Address = obj.Address;
            user.PostalCode = obj.PostalCode;
            db.SaveChanges();
            return true;
        }

        public bool UpdateMyCertificates(EditTrainerCertificateModel obj)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    List<TrainerCertificate> current = db.trainerCertificate.Where(a => a.ExtendIdentityUserId == obj.UserId).ToList();
                    db.trainerCertificate.RemoveRange(current);
                    db.SaveChanges();

                    foreach (var item in obj.certificates)
                    {
                        TrainerCertificate tc = new TrainerCertificate();
                        tc.ExtendIdentityUserId = obj.UserId;
                        tc.CertificateName = item;
                        db.trainerCertificate.Add(tc);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }

                catch (Exception ex)
                {
                    var x = ex.Message;
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdatePassword(UpdatePasswordModel obj)
        {
            ExtendIdentityUser user = userManager.FindByIdAsync(obj.Id).Result;
            var x = userManager.ChangePasswordAsync(user,obj.OldPassword,obj.NewPassword).Result;
            if (x.Succeeded)
            {
                return true;
            }
            return false;
        }

        public bool UploadGalleryImage(UploadImageModel obj)
        {
            var PicStream = new MemoryStream();
            obj.Picture.CopyTo(PicStream);
            var PicBytes = PicStream.ToArray();
            UserGallery res = new UserGallery();
            res.ExtendIdentityUserId = obj.UserId;
            res.GalleryImageData = PicBytes;
            res.GalleryImageContentType = obj.Picture.ContentType;
            res.GalleryImageExtension = obj.Picture.FileName.Split('.')[obj.Picture.FileName.Split('.').Length - 1];
            PicStream.Dispose();
            db.userGallery.Add(res);
            db.SaveChanges();
            return true;
        }
    }
}
