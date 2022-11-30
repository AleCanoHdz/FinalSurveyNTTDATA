using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.Models;
using FinalSurveyNTTDATA.Services.AuthService;
using FinalSurveyNTTDATA.DTOs.AuthUser;
using FinalSurveyNTTDATA.DTOs.Category;
using AutoMapper;
using Azure.Core;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(DataContext context, IAuthService authService, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var rp = await _authService.Register(
                new User
                {
                    Name = request.Name,
                    FirstSurname = request.FirstSurname,
                    LastSurname = request.LastSurname,
                    Status = request.Status,
                    Photo = request.Photo,
                }, request.Password
            );

            if (!rp.Success)
            {
                return BadRequest(rp);
            }
            return Ok(rp);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(request.User, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }



        // GET: api/Auth
        [HttpGet("User")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetUserDto>>>> GetUser()
        {
            var response = new ServiceResponse<IEnumerable<GetUserDto>>();

            var user = await _context.User.Include(c => c.Roles).ToListAsync();

            response.Data = user.Select(c => _mapper.Map<GetUserDto>(c)).ToList();

            return Ok(response);
        }

        // GET: api/Auth/5
        [HttpGet("User{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUser(Guid id)
        {
            var response = new ServiceResponse<GetUserDto>();
            var user = await _context.User.FirstOrDefaultAsync(c => c.IdUser == id);

            if (user != null)
            {
                response.Data = _mapper.Map<GetUserDto>(user);
            }
            else
            {
                response.Success = false;
                response.Message = "User not found";

                return NotFound(response);
            }

            return Ok(response);
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("User{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> PutUser(UpdateUserDto user, Guid id)
        {
            var rp = await _authService.UpdateUser(
                new User
                {
                    Name = user.Name,
                    FirstSurname = user.FirstSurname,
                    LastSurname = user.LastSurname,
                    Status = user.Status,
                    Photo = user.Photo,
                }, user.Password, id
            );

            if (!rp.Success)
            {
                return BadRequest(rp);
            }
            return Ok(rp);
        }

        // DELETE: api/Auth/5
        [HttpDelete("User{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> DeleteUser(Guid id)
        {
            ServiceResponse<IEnumerable<GetUserDto>> serviceResponse = new ServiceResponse<IEnumerable<GetUserDto>>();

            try
            { 
                User user = await _context.User.Include(r => r.Roles).FirstOrDefaultAsync(c => c.IdUser.ToString().ToUpper().Equals(id.ToString().ToUpper()));

                if (user != null)
                {
                    _context.User.Remove(user);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.User.Include(r => r.Roles).Select(c => _mapper.Map<GetUserDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";

                    return NotFound(serviceResponse);
                }
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return Ok(serviceResponse);
        }

        [HttpPost("RoleUser")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> AddUserRole(AddRoleUserDto newRoleUser)
        {
            var serviceResp = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _context.User
                                .Include(r => r.Roles)
                                .FirstOrDefaultAsync(u => u.IdUser == newRoleUser.UsersIdUser);
                if (user == null)
                {
                    serviceResp.Success = false;
                    serviceResp.Message = "User not found";
                    return NotFound(serviceResp);
                }

                var role = await _context.Role
                                .FirstOrDefaultAsync(r => r.IdRole == newRoleUser.RolesIdRole);
                if (role == null)
                {
                    serviceResp.Success = false;
                    serviceResp.Message = "Role not found";
                    return NotFound(serviceResp);
                }

                user.Roles.Add(role);
                await _context.SaveChangesAsync();
                serviceResp.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResp.Success = false;
                serviceResp.Message = ex.Message;
            }

            return serviceResp;
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.IdUser == id);
        }
    }
}
