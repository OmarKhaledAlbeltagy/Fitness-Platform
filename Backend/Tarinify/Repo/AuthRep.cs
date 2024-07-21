using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.Privilage;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class AuthRep : IAuthRep
    {
        private readonly DbContainer db;
        private readonly UserManager<ExtendIdentityUser> userManager;
        private readonly SignInManager<ExtendIdentityUser> signInManager;
        private readonly ITimeRep ti;
        private readonly IBMICalcRep bmi;

        public AuthRep(DbContainer db, UserManager<ExtendIdentityUser> userManager, SignInManager<ExtendIdentityUser> signInManager, ITimeRep ti, IBMICalcRep bmi)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.ti = ti;
            this.bmi = bmi;
        }

        public async Task<dynamic> ClientRegistration(ClientRegisterModel obj)
        {
            try
            {
                DateTime now = ti.GetCurrentTime();
                //if (obj.PhoneNumber[0] == '0')
                //{
                //    obj.PhoneNumber = obj.PhoneNumber.Remove(0, 1);
                //}

                //if (userManager.Users.Any(item => item.PhoneNumber == obj.PhoneNumber && item.PhoneCodeId == obj.PhoneCodeId))
                //{
                //    return new { code = "DuplicatePhoneNumber", description = $"PhoneNumber '{obj.PhoneNumber}' is already taken." };
                //}

                if (obj.WeightKG == 0 && obj.HeightCM == 0 && obj.WeightLB != 0 && obj.HeightIN != 0 && obj.HeightFT != 0)
                {
                    obj.HeightCM = (((double)obj.HeightFT * 12) + (double)obj.HeightIN) * 2.54;
                    obj.WeightKG = obj.WeightLB * 0.45;
                }
                else
                {
                    if (obj.WeightKG != 0 && obj.HeightCM != 0 && obj.WeightLB == 0 && obj.HeightIN == 0 && obj.HeightFT == 0)
                    {

                    }
                    else
                    {
                        return new { Error = "MeasuresError" };
                    }
                }

                BMIModel bmimodel = new BMIModel
                {
                    Gender = obj.Gender,
                    ActivityLevelId = obj.ActivityLevelId,
                    Age = (int)(Math.Floor((double)(now - obj.Birthday).Days / 365)),
                    WeightGoalId = obj.WeightGoalId,
                    HeightCM = obj.HeightCM,
                    WeightKG = obj.WeightKG,
                    HeightIN = 0,
                    HeightFT = 0,
                    WeightLB = 0
                };
                var BmiRes = bmi.CalculateBMI(bmimodel);

                ExtendIdentityUser user = new ExtendIdentityUser();
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.UserName = obj.Email;
                user.Email = obj.Email;
                //user.PhoneCodeId = obj.PhoneCodeId;
                //user.PhoneNumber = obj.PhoneNumber;
                user.Birthday = obj.Birthday;
                user.Gender = obj.Gender;
                user.StateId = obj.StateId;
                user.ActivityLevelId = obj.ActivityLevelId;
                user.BodyTypeId = obj.BodyTypeId;
                user.WeightGoalId = obj.WeightGoalId;
                user.HeightCM = obj.HeightCM;
                user.WeightKG = obj.WeightKG;
                user.NeededCalories = (int)BmiRes.NeededCalories;
                user.RegistrationDateTime = now;

                var create = userManager.CreateAsync(user, obj.Password).Result;

                if (create.Succeeded)
                {
                    var addtorole = await userManager.AddToRoleAsync(user, obj.Role);
                    if (addtorole.Succeeded)
                    {
                        SendConfirmationEmail(user);
                        return true;
                    }
                }

                if (create.Errors.Count() > 0)
                {
                    return create.Errors;
                }
                return true;
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("Sending Email Error", smtpEx);
            }
            catch (Exception ex)
            {

                throw new Exception("Registration Errors", ex);
            }

        }

        public async Task<dynamic> TrainerRegistration(TrainerRegistrationModel obj)
        {
            try
            {
                DateTime now = ti.GetCurrentTime();
                //if (obj.PhoneNumber[0] == '0')
                //{
                //    obj.PhoneNumber = obj.PhoneNumber.Remove(0, 1);
                //}

                //if (userManager.Users.Any(item => item.PhoneNumber == obj.PhoneNumber && item.PhoneCodeId == obj.PhoneCodeId))
                //{
                //    return new { code = "DuplicatePhoneNumber", description = $"PhoneNumber '{obj.PhoneNumber}' is already taken." };
                //}





                ExtendIdentityUser user = new ExtendIdentityUser();
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.UserName = obj.Email;
                user.Email = obj.Email;
                user.StateId = obj.StateId;
                //user.PhoneCodeId = obj.PhoneCodeId;
                //user.PhoneNumber = obj.PhoneNumber;
                user.Birthday = obj.Birthday;
                user.Gender = obj.Gender;
                user.TrainerTitleId = obj.TrainerTitleId;
                user.RegistrationDateTime = now;

                if (obj.profilePicture != null && obj.profilePicture.Length > 0)
                {
                    var ImageStream = new MemoryStream();
                    obj.profilePicture.CopyTo(ImageStream);
                    var ImageBytes = ImageStream.ToArray();
                    user.ProfilePictureData = ImageBytes;
                    user.ProfilePictureContentType = obj.profilePicture.ContentType;
                    user.ProfilePictureExtension = obj.profilePicture.FileName.Split('.')[obj.profilePicture.FileName.Split('.').Length - 1];
                    ImageStream.Dispose();
                }
                var create = userManager.CreateAsync(user, obj.Password).Result;

                if (create.Succeeded)
                {
                    var addtorole = await userManager.AddToRoleAsync(user, obj.Role);
                    if (addtorole.Succeeded)
                    {
                        foreach (var item in obj.Certificates)
                        {
                            TrainerCertificate TC = new TrainerCertificate();
                            TC.ExtendIdentityUserId = user.Id;
                            TC.CertificateName = item;
                            db.trainerCertificate.Add(TC);
                        }
                        db.SaveChanges();

                        SendConfirmationEmail(user);
                        return true;
                    }
                }

                if (create.Errors.Count() > 0)
                {
                    return create.Errors;
                }
                return true;
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("Sending Email Error", smtpEx);
            }
            catch (Exception ex)
            {

                throw new Exception("Registration Errors", ex);
            }

        }

        public async Task<dynamic> Login(LoginModel obj)
        {
            try
            {
                var res = await signInManager.PasswordSignInAsync(obj.Email, obj.Password, false, false);
                if (res.Succeeded)
                {
                    ExtendIdentityUser user = await userManager.FindByEmailAsync(obj.Email);
                    var roles = await userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();

                    LoginResultModel result = new LoginResultModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        FullName = $"{user.FirstName} {user.LastName}",
                        Token = user.Id,
                        Role = role,
                        Email = user.Email,
                        IsProfilePicture = user.ProfilePictureData != null && user.ProfilePictureData.Length != 0
                    };
                    return result;
                }
                else
                {
                    if (res.IsNotAllowed)
                    {
                        return new { Error = "EmailNotConfirmed" };
                    }
                    if (res == SignInResult.Failed)
                    {
                        return new { Error = "NotAuth" };
                    }
                    return new { Error = "Error" };
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
                var z = ex.Data;
                var a = ex.Source;
                throw new Exception("Login Error", ex);
            }

        }

        public async Task<dynamic> ConfirmEmail(EmailConfirmationModel obj)
        {
            try
            {
                ExtendIdentityUser user = await userManager.FindByEmailAsync(obj.Email);
                var res = await userManager.ConfirmEmailAsync(user, obj.Token);
                return res;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
                var z = ex.Data;
                var a = ex.Source;
                throw new Exception("Confirmation Error", ex);
            }
        }

        public async Task<bool> SendConfirmationEmail(ExtendIdentityUser user)
        {

            try
            {
                string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                string body = "email confirmation token \n" + token;
                MailMessage m = new MailMessage();
                m.To.Add(user.Email);
                m.Subject = "Email Confirmation";
                m.From = new MailAddress("example@abc.com");
                m.Sender = new MailAddress("example@abc.com");
                m.IsBodyHtml = true;
                m.Body = body;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("SMTP-Server", 587);
                smtp.EnableSsl = false;
                smtp.Credentials = new NetworkCredential("example@abc.com", "Password");
                smtp.Send(m);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("Sending Email Error", smtpEx);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
                var z = ex.Data;
                var a = ex.Source;
                throw new Exception("Email Error", ex);
            }

        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                ExtendIdentityUser user = await userManager.FindByEmailAsync(email);
                string token = await userManager.GeneratePasswordResetTokenAsync(user);

                string body = "Forget password token \n" + token;
                MailMessage m = new MailMessage();
                m.To.Add(user.Email);
                m.Subject = "Forget password";
                m.From = new MailAddress("example@abc.com");
                m.Sender = new MailAddress("example@abc.com");
                m.IsBodyHtml = true;
                m.Body = body;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("SMTP-Server", 587);
                smtp.EnableSsl = false;
                smtp.Credentials = new NetworkCredential("example@abc.com", "Password");
                smtp.Send(m);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("Sending Email Error", smtpEx);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
                var z = ex.Data;
                var a = ex.Source;
                throw new Exception("Password Reset Error", ex);
            }
            
        }

        public async Task<dynamic> ResetForgottenPassword(ResetForgottenPasswordModel obj)
        {
            try
            {
                ExtendIdentityUser user = await userManager.FindByEmailAsync(obj.Email);
                var x = await userManager.ResetPasswordAsync(user, obj.Token, obj.NewPassword);
                if (x.Succeeded)
                {
                    return true;
                }
                else
                {

                    return x.Errors;

                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
                var z = ex.Data;
                var a = ex.Source;
                throw new Exception("Password Reset Error", ex);
            }

        }

        public async Task<dynamic> ResetPassword(ResetPasswordModel obj)
        {
            try
            {
                ExtendIdentityUser user = await userManager.FindByIdAsync(obj.UserToken);
                var res = await userManager.ChangePasswordAsync(user, obj.CurrentPassword, obj.NewPassword);
                if (res.Succeeded)
                {
                    return true;
                }
                else
                {
                    return res.Errors;
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
                var z = ex.Data;
                var a = ex.Source;
                throw new Exception("Password Reset Error", ex);
            }
        }

        public async Task<dynamic> SendResetEmailToken(SendResetEmailTokenModel obj)
        {
            try
            {
                ExtendIdentityUser user = await userManager.FindByIdAsync(obj.UserToken);
                var token = await userManager.GenerateChangeEmailTokenAsync(user, obj.NewEmail);
                string body = "Reset Email token \n" + token;
                MailMessage m = new MailMessage();
                m.To.Add(user.Email);
                m.Subject = "Reset Email";
                m.From = new MailAddress("example@abc.com");
                m.Sender = new MailAddress("example@abc.com");
                m.IsBodyHtml = true;
                m.Body = body;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("SMTP-Server", 587);
                smtp.EnableSsl = false;
                smtp.Credentials = new NetworkCredential("example@abc.com", "Password");
                smtp.Send(m);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("Sending Email Error", smtpEx);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
                var z = ex.Data;
                var a = ex.Source;
                throw new Exception("Email Reset Error", ex);
            }
            
        }

        public async Task<dynamic> ResetEmail(ResetEmailModel obj)
        {
   
            try
            {
                ExtendIdentityUser user = await userManager.FindByEmailAsync(obj.UserToken);
                var res = await userManager.ChangeEmailAsync(user, obj.NewEmail, obj.Token);
                if (res.Succeeded)
                {
                    return true;
                }
                else
                {
                    return res.Errors;
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                var y = ex.InnerException;
                var z = ex.Data;
                var a = ex.Source;
                throw new Exception("Email Reset Error", ex);
            }
        }

        public async Task<dynamic> SignupGeneralInfo(SignupGeneralInfoModel obj)
        {
            try
            {
                DateTime now = ti.GetCurrentTime();

                var existingSignup = await db.notCompletedSignup.FirstOrDefaultAsync(a => a.Email == obj.Email);

                if (existingSignup == null)
                {
                    NotCompletedSignup newSignup = new NotCompletedSignup
                    {
                        FirstName = obj.FirstName,
                        LastName = obj.LastName,
                        Email = obj.Email,
                        ExtendIdentityRoleId = obj.RoleId,
                        StateId = obj.StateId,
                        BirthDate = obj.BirthDate,
                        CreationDateTime = now,
                        ModifiedDateTime = now
                    };

                    db.notCompletedSignup.Add(newSignup);
                }
                else
                {
                    existingSignup.FirstName = obj.FirstName;
                    existingSignup.LastName = obj.LastName;
                    existingSignup.StateId = obj.StateId;
                    existingSignup.BirthDate = obj.BirthDate;
                    existingSignup.ModifiedDateTime = now;
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving general info.", ex);
            }
        }

        public List<RoleViewModel> GetSignupRoles()
        {
            List<ExtendIdentityRole> roles = db.Roles.Where(a=>a.Name == "Client" || a.Name == "Trainer").ToList();
            List<RoleViewModel> res = new List<RoleViewModel>();
            foreach (var item in roles)
            {
                RoleViewModel obj = new RoleViewModel();
                obj.Id = item.Id;
                obj.RoleName = item.Name;
                res.Add(obj);
            }

            return res; 
        }
    }
}
