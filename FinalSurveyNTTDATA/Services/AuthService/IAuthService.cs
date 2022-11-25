using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> Exist(string username);
    }
}
