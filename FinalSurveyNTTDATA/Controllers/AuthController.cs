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

            var user = await _context.User.ToListAsync();

            response.Data = user.Select(c => _mapper.Map<GetUserDto>(c)).ToList();

            return Ok(response);
        }

        // GET: api/Auth/5
        [HttpGet("User{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUser(int id)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> PutUser(UpdateUserDto user, int id)
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.IdUser == id);
        }
    }
}
