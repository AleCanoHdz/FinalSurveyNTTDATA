using FinalSurveyNTTDATA.DTOs.AuthUser;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<List<GetUserDto>>> GetUsers();
        Task<ServiceResponse<GetUserDto>> GetUser(Guid id);
        Task<ServiceResponse<GetUserDto>> UpdateUser(User user, string password, Guid id);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(Guid id);
        Task<ServiceResponse<GetUserDto>> AddUserRole(AddRoleUserDto userRole);
        Task<bool> Exist(string username);
        Task<bool> UserIdExist(Guid id);
    }
}
