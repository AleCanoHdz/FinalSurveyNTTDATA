using AutoMapper;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.DTOs.AuthUser;
using FinalSurveyNTTDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinalSurveyNTTDATA.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<bool> Exist(string username)
        {
            if (await _context.User.AnyAsync(c => c.Name.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var rp = new ServiceResponse<string>();
            var user = await _context.User.Include(c => c.Roles)
                .FirstOrDefaultAsync(c => c.Name.ToLower().Equals(username.ToLower()));

            if (username == null)
            {
                rp.Success = false;
                rp.Message = "User not found";
            }
            else if (!verifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                rp.Success = false;
                rp.Message = "Wrong password";
            }
            else
            {
                rp.Data = CreateToken(user);
            }

            return rp;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> rp = new ServiceResponse<int>();
            if (await Exist(user.Name))
            {
                rp.Success = false;
                rp.Message = "User already exist";
                return rp;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.User.Add(user);

            await _context.SaveChangesAsync();
            rp.Data = user.IdUser;

            return rp;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            //Listado de Parametros que tendrà el Json Token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.FirstSurname)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(User user, string password, int id)
        {
            ServiceResponse<GetUserDto> rp = new ServiceResponse<GetUserDto>();
            try
            {
                if (await UserIdExist(id))
                {
                    CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.IdUser = id;

                    _context.Entry(user).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    rp.Data = _mapper.Map<GetUserDto>(user);
                }
                else
                {
                    rp.Success = false;
                    rp.Message = "User not found";
                }
            }
            catch(DbUpdateException ex)
            {
                rp.Success = false;
                rp.Message = ex.Message;
            }

            return rp;
        }

        public async Task<bool> UserIdExist(int id)
        {
            if (await _context.User.AnyAsync(c => c.IdUser.Equals(id)))
            {
                return true;
            }
            return false;
        }
    }
}
