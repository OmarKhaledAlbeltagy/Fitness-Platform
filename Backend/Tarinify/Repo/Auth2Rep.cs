using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Trainify.Context;
using Trainify.Entities;
using Trainify.Models;
using Trainify.Privilage;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public class Auth2Rep : IAuth2Rep
    {
        private readonly DbContainer _db;
        private readonly UserManager<ExtendIdentityUser> _userManager;
        private readonly SignInManager<ExtendIdentityUser> _signInManager;
        private readonly ITimeRep _timeRep;
        private readonly IBMICalcRep _bmiCalcRep;

        public Auth2Rep(DbContainer db, UserManager<ExtendIdentityUser> userManager, SignInManager<ExtendIdentityUser> signInManager, ITimeRep timeRep, IBMICalcRep bmiCalcRep)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _timeRep = timeRep;
            _bmiCalcRep = bmiCalcRep;
        }

        public async Task<dynamic> ClientRegistration(ClientRegisterModel obj)
        {
            try
            {
                DateTime now = _timeRep.GetCurrentTime();

                // Normalize measurements...
                NormalizeMeasurements(obj);

                // Calculate BMI...
                BMIModel bmiModel = CreateBMIModel(obj, now);
                var bmiResult = _bmiCalcRep.CalculateBMI(bmiModel);

                // Create user...
                ExtendIdentityUser user = CreateClientUser(obj, now, bmiResult);

                // Register user...
                var createUserResult = await _userManager.CreateAsync(user, obj.Password);
                if (!createUserResult.Succeeded)
                    return createUserResult.Errors;

                // Assign role...
                var addRoleResult = await _userManager.AddToRoleAsync(user, obj.Role);
                if (!addRoleResult.Succeeded)
                    return addRoleResult.Errors;

                // Send confirmation email...
                await SendConfirmationEmail(user);

                return true;
            }
            catch (Exception ex)
            {
                // Logging...
                throw new Exception("Client Registration Error", ex);
            }
        }

        public async Task<dynamic> TrainerRegistration(TrainerRegistrationModel obj)
        {
            try
            {
                DateTime now = _timeRep.GetCurrentTime();

                ExtendIdentityUser user = CreateTrainerUser(obj, now);

                // Set profile picture...
                SetProfilePicture(user, obj.profilePicture);

                var createUserResult = await _userManager.CreateAsync(user, obj.Password);
                if (!createUserResult.Succeeded)
                    return createUserResult.Errors;

                var addRoleResult = await _userManager.AddToRoleAsync(user, obj.Role);
                if (!addRoleResult.Succeeded)
                    return addRoleResult.Errors;

                // Add trainer certificates...
                AddTrainerCertificates(user, obj.Certificates.ToArray());

                await SendConfirmationEmail(user);

                return true;
            }
            catch (Exception ex)
            {
                // Logging...
                throw new Exception("Trainer Registration Error", ex);
            }
        }

        public async Task<dynamic> Login(LoginModel obj)
        {
            try
            {
                var signInResult = await _signInManager.PasswordSignInAsync(obj.Email, obj.Password, false, false);
                if (signInResult.Succeeded)
                {
                    ExtendIdentityUser user = await _userManager.FindByEmailAsync(obj.Email);
                    var roles = await _userManager.GetRolesAsync(user);
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
                    if (signInResult.IsNotAllowed)
                        return new { Error = "EmailNotConfirmed" };
                    if (signInResult == SignInResult.Failed)
                        return new { Error = "NotAuth" };
                    return new { Error = "Error" };
                }
            }
            catch (Exception ex)
            {
                // Logging...
                throw new Exception("Login Error", ex);
            }
        }

        // Other methods...

        private void NormalizeMeasurements(ClientRegisterModel obj)
        {
            if (obj.WeightKG == 0 && obj.HeightCM == 0 && obj.WeightLB != 0 && obj.HeightIN != 0 && obj.HeightFT != 0)
            {
                obj.HeightCM = (((double)obj.HeightFT * 12) + (double)obj.HeightIN) * 2.54;
                obj.WeightKG = obj.WeightLB * 0.45;
            }
            else
            {
                if (!(obj.WeightKG != 0 && obj.HeightCM != 0 && obj.WeightLB == 0 && obj.HeightIN == 0 && obj.HeightFT == 0))
                    throw new Exception("Measures Error");
            }
        }

        private BMIModel CreateBMIModel(ClientRegisterModel obj, DateTime now)
        {
            return new BMIModel
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
        }

        private ExtendIdentityUser CreateClientUser(ClientRegisterModel obj, DateTime now, dynamic bmiResult = null)
        {
            ExtendIdentityUser user = new ExtendIdentityUser
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                UserName = obj.Email,
                Email = obj.Email,
                Birthday = obj.Birthday,
                Gender = obj.Gender,
                StateId = obj.StateId,
                ActivityLevelId = obj.ActivityLevelId,
                BodyTypeId = obj.BodyTypeId,
                WeightGoalId = obj.WeightGoalId,
                HeightCM = obj.HeightCM,
                WeightKG = obj.WeightKG,
                NeededCalories = bmiResult != null ? (int)bmiResult.NeededCalories : 0,
                RegistrationDateTime = now
            };
            return user;
        }

        private ExtendIdentityUser CreateTrainerUser(TrainerRegistrationModel obj, DateTime now, dynamic bmiResult = null)
        {
            ExtendIdentityUser user = new ExtendIdentityUser
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                UserName = obj.Email,
                Email = obj.Email,
                StateId = obj.StateId,
                Birthday = obj.Birthday,
                Gender = obj.Gender,
                TrainerTitleId = obj.TrainerTitleId,
                RegistrationDateTime = now
            };
            return user;
        }

        private void SetProfilePicture(ExtendIdentityUser user, IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    profilePicture.CopyTo(memoryStream);
                    user.ProfilePictureData = memoryStream.ToArray();
                    user.ProfilePictureContentType = profilePicture.ContentType;
                    user.ProfilePictureExtension = Path.GetExtension(profilePicture.FileName);
                }
            }
        }

        private void AddTrainerCertificates(ExtendIdentityUser user, string[] certificates)
        {
            foreach (var item in certificates)
            {
                TrainerCertificate TC = new TrainerCertificate
                {
                    ExtendIdentityUserId = user.Id,
                    CertificateName = item
                };
                _db.trainerCertificate.Add(TC);
            }
            _db.SaveChanges();
        }

        private async Task SendConfirmationEmail(ExtendIdentityUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string body = $"Email confirmation token: {token}";

            await SendEmailAsync(user.Email, "Email Confirmation", body);
        }

        private async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(to);

                using (SmtpClient smtpClient = new SmtpClient("SMTP-Server", 587))
                {
                    smtpClient.EnableSsl = false;
                    smtpClient.Credentials = new NetworkCredential("example@abc.com", "Password");
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Logging...
                throw new Exception("Email Sending Error", ex);
            }
        }
    }
}