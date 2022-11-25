using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.Models;
using AutoMapper;
using FinalSurveyNTTDATA.DTOs.QuestionAnswer;
using FinalSurveyNTTDATA.DTOs.UserAnswer;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnswersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserAnswersController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/UserAnswers
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetUserAnswerDto>>>> GetUserAnswer()
        {
            var response = new ServiceResponse<IEnumerable<GetUserAnswerDto>>();

            var user = await _context.UserAnswer.ToListAsync();

            response.Data = user.Select(c => _mapper.Map<GetUserAnswerDto>(c)).ToList();

            return Ok(response);
        }

        // GET: api/UserAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> GetUserAnswer(Guid id)
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

                return NotFound(response);
            }

            return Ok(response);
        }

        // PUT: api/UserAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> PutUserAnswer(UpdateUserAnswerDto ua, Guid id)
        {
            ServiceResponse<GetUserAnswerDto> response = new ServiceResponse<GetUserAnswerDto>();
            try
            {
                var usera = await _context.UserAnswer.FindAsync(id);

                if (UserAnswerExists(id))
                {
                    _mapper.Map(ua, usera);

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
                return NotFound(response);
            }

            return Ok(response);
        }

        // POST: api/UserAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetUserAnswerDto>>>> PostUserAnswer(AddUserAnswerDto ua)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetUserAnswerDto>>();

            UserAnswer usera = _mapper.Map<UserAnswer>(ua);

            _context.UserAnswer.Add(usera);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.UserAnswer.Select(c => _mapper.Map<GetUserAnswerDto>(c)).ToListAsync();

            return Ok(serviceResponse);
        }

        // DELETE: api/UserAnswers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> DeleteUserAnswer(Guid id)
        {
            ServiceResponse<IEnumerable<GetUserAnswerDto>> serviceResponse = new ServiceResponse<IEnumerable<GetUserAnswerDto>>();

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

        private bool UserAnswerExists(Guid id)
        {
            return _context.UserAnswer.Any(e => e.IdUserAnswer == id);
        }
    }
}
