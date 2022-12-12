using AutoMapper;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.DTOs.UserAnswer;
using FinalSurveyNTTDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Services.UserAnswerService
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserAnswerService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetUserAnswerDto>>> AddUserAnswer(AddUserAnswerDto userAnswer)
        {
            var serviceResponse = new ServiceResponse<List<GetUserAnswerDto>>();

            UserAnswer usera = _mapper.Map<UserAnswer>(userAnswer);

            _context.UserAnswer.Add(usera);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.UserAnswer.Select(c => _mapper.Map<GetUserAnswerDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserAnswerDto>>> DeleteUserAnswer(Guid id)
        {
            ServiceResponse<List<GetUserAnswerDto>> serviceResponse = new ServiceResponse<List<GetUserAnswerDto>>();

            try
            {
                UserAnswer ua = await _context.UserAnswer.FirstOrDefaultAsync(c => c.IdUserAnswer.ToString().ToUpper() == id.ToString().ToUpper());

                if (ua != null)
                {
                    _context.UserAnswer.Remove(ua);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.UserAnswer.Select(c => _mapper.Map<GetUserAnswerDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User answer not found";

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

        public async Task<ServiceResponse<GetUserAnswerDto>> GetUserAnswer(Guid id)
        {
            var response = new ServiceResponse<GetUserAnswerDto>();
            var ua = await _context.UserAnswer.FirstOrDefaultAsync(c => c.IdUserAnswer.ToString().ToUpper() == id.ToString().ToUpper());

            if (ua != null)
            {
                response.Data = _mapper.Map<GetUserAnswerDto>(ua);
            }
            else
            {
                response.Success = false;
                response.Message = "User answer not found";

                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetUserAnswerDto>>> GetUserAnswers()
        {
            var response = new ServiceResponse<List<GetUserAnswerDto>>();

            var user = await _context.UserAnswer.Include(u => u.User).Include(q => q.Question).ToListAsync();

            response.Data = user.Select(c => _mapper.Map<GetUserAnswerDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetUserAnswerDto>> UpdateUserAnswer(UpdateUserAnswerDto userAnswer, Guid id)
        {
            ServiceResponse<GetUserAnswerDto> response = new ServiceResponse<GetUserAnswerDto>();
            try
            {
                var usera = await _context.UserAnswer.FindAsync(id);

                if (UserAnswerExists(id))
                {
                    _mapper.Map(userAnswer, usera);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetUserAnswerDto>(usera);
                }
                else
                {
                    response.Success = false;
                    response.Message = "User answer not found";
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

        private bool UserAnswerExists(Guid id)
        {
            return _context.UserAnswer.Any(e => e.IdUserAnswer == id);
        }
    }
}
