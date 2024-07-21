using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using Trainify.Context;
using Trainify.Privilage;
using static System.Net.Mime.MediaTypeNames;
using Trainify.Models;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class UserRep : IUserRep
    {
        private UserManager<ExtendIdentityUser> userManager;
        private SignInManager<ExtendIdentityUser> signInManager;
        private DbContainer db;
        private readonly ITimeRep ti;

        public UserRep(UserManager<ExtendIdentityUser> userManager, SignInManager<ExtendIdentityUser> signInManager, DbContainer db, ITimeRep ti)
        {
            this.userManager = userManager;
            this.db = db;
            this.ti = ti;
            this.signInManager = signInManager;
        }

   

        //public async Task<dynamic> Register(RegisterModel obj)
        //{
        //    DateTime now = ti.GetCurrentTime();
        //    if (obj.PhoneNumber[0] == '0')
        //    {
        //      obj.PhoneNumber =  obj.PhoneNumber.Remove(0, 1);
        //    }
         
        //    ExtendIdentityUser? check = userManager.Users.Where(a => a.Email == obj.Email).FirstOrDefault();    
        //    ExtendIdentityUser? check2 = userManager.Users.Where(a => a.PhoneNumber == obj.PhoneNumber && a.PhoneCodeId == obj.PhoneCodeId).FirstOrDefault();
        //    if (check != null && check2 != null)
        //    {
        //        return "emailphone";
        //    }
        //    if (check != null)
        //    {
        //        return "email";
        //    }
        //    if (check2 != null)
        //    {
        //        return "phone";
        //    }

        //    int indx = obj.ProfilePicture.FileName.Split('.').Length - 1;
        //    string Extension = obj.ProfilePicture.FileName.Split('.')[indx];

        //    long fileSize = obj.ProfilePicture.Length;
        //    string contentType = obj.ProfilePicture.ContentType;

        //    ExtendIdentityUser user = new ExtendIdentityUser();

        //    if (fileSize < 5500000)
        //    {
        //        using (var stream = new MemoryStream())
        //        {
        //            obj.ProfilePicture.CopyTo(stream);
        //            var bytes = stream.ToArray();
               
        //            user.PhoneNumber = obj.PhoneNumber;
        //            user.Address = obj.Address;
        //            user.Email = obj.Email;
        //            user.FirstName = obj.FirstName;
        //            user.LastName = obj.LastName;
        //            user.UserName = obj.Email;
        //            user.PostalCode = obj.PostalCode;
        //            user.StateId = obj.StateId;
        //            user.PhoneCodeId = obj.PhoneCodeId;
        //            user.ProfilePictureData = bytes;
        //            user.ProfilePictureContentType = contentType;
        //            user.ProfilePictureExtension = Extension;
        //            user.RegistrationDateTime = now;
        //            try
        //            {
        //                var create = userManager.CreateAsync(user, obj.Password).Result;
        //                if (create.Succeeded)
        //                {
        //                    var addtorole = userManager.AddToRoleAsync(user, obj.Role).Result;
        //                    if (addtorole.Succeeded)
        //                    {
        //                        string token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
        //                        string body = token;
        //                        MailMessage m = new MailMessage();
        //                        m.To.Add(user.Email);
        //                        m.Subject = "Trainify Email Confirmation";
        //                        m.From = new MailAddress("example@abc.com");
        //                        m.Sender = new MailAddress("example@abc.com");
        //                        m.IsBodyHtml = true;
        //                        m.Body = body;
        //                        m.IsBodyHtml = true;
        //                        SmtpClient smtp = new SmtpClient("SMTP-Server", 587);
        //                        smtp.EnableSsl = false;
        //                        smtp.Credentials = new NetworkCredential("example@abc.com", "Password");
        //                        smtp.Send(m);
        //                        return true;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return ex.InnerException.Message;
        //            }
        //        }
        //    }

        //    return false;

        //}
    }
}
