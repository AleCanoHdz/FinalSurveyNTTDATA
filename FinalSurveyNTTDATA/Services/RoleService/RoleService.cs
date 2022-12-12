using AutoMapper;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.DTOs.Role;
using FinalSurveyNTTDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RoleService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetRoleDto>>> AddRole(AddRoleDto role)
        {
            var serviceResponse = new ServiceResponse<List<GetRoleDto>>();

            Role rol = _mapper.Map<Role>(role);

            _context.Role.Add(rol);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Role.Select(c => _mapper.Map<GetRoleDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRoleDto>>> DeleteRole(Guid id)
        {
            ServiceResponse<List<GetRoleDto>> serviceResponse = new ServiceResponse<List<GetRoleDto>>();

            try
            {
                Role rol = await _context.Role.FirstOrDefaultAsync(c => c.IdRole.ToString().ToUpper() == id.ToString().ToUpper());

                if (rol != null)
                {
                    _context.Role.Remove(rol);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Role.Select(c => _mapper.Map<GetRoleDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Role not found";

                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRoleDto>> GetRole(Guid id)
        {
            var response = new ServiceResponse<GetRoleDto>();
            var role = await _context.Role.FirstOrDefaultAsync(c => c.IdRole.ToString().ToUpper() == id.ToString().ToUpper());

            if (role != null)
            {
                response.Data = _mapper.Map<GetRoleDto>(role);
            }
            else
            {
                response.Success = false;
                response.Message = "Role not found";

                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetRoleDto>>> GetRoles()
        {
            var response = new ServiceResponse<List<GetRoleDto>>();

            var role = await _context.Role.ToListAsync();

            response.Data = role.Select(c => _mapper.Map<GetRoleDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetRoleDto>> UpdateRole(UpdateRoleDto role, Guid id)
        {
            ServiceResponse<GetRoleDto> response = new ServiceResponse<GetRoleDto>();
            try
            {
                var rol = await _context.Role.FindAsync(id);

                if (RoleExists(id))
                {
                    _mapper.Map(role, rol);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetRoleDto>(rol);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Role not found";
                }
            }
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            if (response.Data == null)
            {
                return response;
            }

            return response;
        }

        private bool RoleExists(Guid id)
        {
            return _context.Role.Any(e => e.IdRole == id);
        }
    }
}
