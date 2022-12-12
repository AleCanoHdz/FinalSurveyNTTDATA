using FinalSurveyNTTDATA.DTOs.Question;
using FinalSurveyNTTDATA.DTOs.Role;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Services.RoleService
{
    public interface IRoleService
    {
        Task<ServiceResponse<List<GetRoleDto>>> GetRoles();
        Task<ServiceResponse<GetRoleDto>> GetRole(Guid id);
        Task<ServiceResponse<List<GetRoleDto>>> AddRole(AddRoleDto role);
        Task<ServiceResponse<GetRoleDto>> UpdateRole(UpdateRoleDto role, Guid id);
        Task<ServiceResponse<List<GetRoleDto>>> DeleteRole(Guid id);
    }
}
