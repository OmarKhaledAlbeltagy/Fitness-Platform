using Trainify.Models;

namespace Trainify.Repo
{
    public interface IAuth2Rep
    {
        Task<dynamic> ClientRegistration(ClientRegisterModel obj);

        Task<dynamic> TrainerRegistration(TrainerRegistrationModel obj);

        Task<dynamic> Login(LoginModel obj);


    }
}
