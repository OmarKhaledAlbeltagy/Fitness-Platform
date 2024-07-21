using Trainify.Models;
using Trainify.Privilage;
using Trainify.ViewModel;

namespace Trainify.Repo
{
    public interface IAuthRep
    {
        Task<dynamic> Login(LoginModel obj);

        Task<dynamic> ClientRegistration(ClientRegisterModel obj);

        Task<dynamic> TrainerRegistration(TrainerRegistrationModel obj);

        Task<dynamic> ConfirmEmail(EmailConfirmationModel obj);

        Task<bool> SendConfirmationEmail(ExtendIdentityUser user);

        Task<bool> ForgotPassword(string email);

        Task<dynamic> ResetForgottenPassword(ResetForgottenPasswordModel obj);

        Task<dynamic> ResetPassword(ResetPasswordModel obj);

        Task<dynamic> SendResetEmailToken(SendResetEmailTokenModel obj);

        Task<dynamic> ResetEmail(ResetEmailModel obj);

        Task<dynamic> SignupGeneralInfo(SignupGeneralInfoModel obj);

        List<RoleViewModel> GetSignupRoles();
    }
}
