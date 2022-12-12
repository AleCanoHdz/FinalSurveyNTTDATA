using AutoMapper;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.DTOs.Question;
using FinalSurveyNTTDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public QuestionService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetQuestionDto>>> AddQuestion(AddQuestionDto question)
        {
            var serviceResponse = new ServiceResponse<List<GetQuestionDto>>();

            Question quest = _mapper.Map<Question>(question);

            _context.Question.Add(quest);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Question.Select(c => _mapper.Map<GetQuestionDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetQuestionDto>>> DeleteQuestion(Guid id)
        {
            ServiceResponse<List<GetQuestionDto>> serviceResponse = new ServiceResponse<List<GetQuestionDto>>();

            try
            {
                Question quest = await _context.Question.FirstOrDefaultAsync(c => c.IdQuestion.ToString().ToUpper() == id.ToString().ToUpper());

                if (quest != null)
                {
                    _context.Question.Remove(quest);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Question.Select(c => _mapper.Map<GetQuestionDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Question No Encontrado";

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

        public async Task<ServiceResponse<GetQuestionDto>> GetQuestion(Guid id)
        {
            var response = new ServiceResponse<GetQuestionDto>();
            var quest = await _context.Question.FirstOrDefaultAsync(c => c.IdQuestion.ToString().ToUpper() == id.ToString().ToUpper());

            if (quest != null)
            {
                response.Data = _mapper.Map<GetQuestionDto>(quest);
            }
            else
            {
                response.Success = false;
                response.Message = "Question not found";

                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetQuestionDto>>> GetQuestions()
        {
            var response = new ServiceResponse<List<GetQuestionDto>>();

            var question = await _context.Question.Include(s => s.Survey).ToListAsync();

            response.Data = question.Select(c => _mapper.Map<GetQuestionDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetQuestionDto>> UpdateQuestion(UpdateQuestionDto question, Guid id)
        {
            ServiceResponse<GetQuestionDto> response = new ServiceResponse<GetQuestionDto>();
            try
            {
                var quest = await _context.Question.FindAsync(id);

                if (QuestionExists(id))
                {
                    _mapper.Map(question, quest);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetQuestionDto>(quest);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Question not found";
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

        private bool QuestionExists(Guid id)
        {
            return _context.Question.Any(e => e.IdQuestion == id);
        }
    }
}
