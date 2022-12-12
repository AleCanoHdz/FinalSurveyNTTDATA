using AutoMapper;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.DTOs.QuestionAnswer;
using FinalSurveyNTTDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Services.QuestionAnswerService
{
    public class QuestionAnswerService : IQuestionAnswerService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public QuestionAnswerService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetQuestionAnswerDto>>> AddQuestionAnswer(AddQuestionAnswerDto questionAnswer)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionAnswerDto>>();

            QuestionAnswer questa = _mapper.Map<QuestionAnswer>(questionAnswer);

            _context.QuestionAnswer.Add(questa);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.QuestionAnswer.Select(c => _mapper.Map<GetQuestionAnswerDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetQuestionAnswerDto>>> DeleteQuestionAnswer(Guid id)
        {
            ServiceResponse<List<GetQuestionAnswerDto>> serviceResponse = new ServiceResponse<List<GetQuestionAnswerDto>>();

            try
            {
                QuestionAnswer quest = await _context.QuestionAnswer.FirstOrDefaultAsync(c => c.IdQuestionAnswer.ToString().ToUpper() == id.ToString().ToUpper());

                if (quest != null)
                {
                    _context.QuestionAnswer.Remove(quest);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.QuestionAnswer.Select(c => _mapper.Map<GetQuestionAnswerDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Question answer not found";

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

        public async Task<ServiceResponse<GetQuestionAnswerDto>> GetQuestionAnswer(Guid id)
        {
            var response = new ServiceResponse<GetQuestionAnswerDto>();
            var qa = await _context.QuestionAnswer.FirstOrDefaultAsync(c => c.IdQuestionAnswer.ToString().ToUpper() == id.ToString().ToUpper());

            if (qa != null)
            {
                response.Data = _mapper.Map<GetQuestionAnswerDto>(qa);
            }
            else
            {
                response.Success = false;
                response.Message = "Question Answer not found";

                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetQuestionAnswerDto>>> GetQuestionAnswers()
        {
            var response = new ServiceResponse<List<GetQuestionAnswerDto>>();

            var qa = await _context.QuestionAnswer.Include(q => q.Question).ThenInclude(s => s.Survey).ThenInclude(c => c.Category).ToListAsync();

            response.Data = qa.Select(c => _mapper.Map<GetQuestionAnswerDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetQuestionAnswerDto>> UpdateQuestionAnswer(UpdateQuestionAnswerDto questionAnswer, Guid id)
        {
            ServiceResponse<GetQuestionAnswerDto> response = new ServiceResponse<GetQuestionAnswerDto>();
            try
            {
                var questa = await _context.QuestionAnswer.FindAsync(id);

                if (QuestionAnswerExists(id))
                {
                    _mapper.Map(questionAnswer, questa);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetQuestionAnswerDto>(questa);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Question answer not found";
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

        private bool QuestionAnswerExists(Guid id)
        {
            return _context.QuestionAnswer.Any(e => e.IdQuestionAnswer == id);
        }
    }
}
