using FinalSurveyNTTDATA.DTOs.Question;
using FinalSurveyNTTDATA.DTOs.QuestionAnswer;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<ServiceResponse<List<GetQuestionDto>>> GetQuestions();
        Task<ServiceResponse<GetQuestionDto>> GetQuestion(Guid id);
        Task<ServiceResponse<List<GetQuestionDto>>> AddQuestion(AddQuestionDto question);
        Task<ServiceResponse<GetQuestionDto>> UpdateQuestion(UpdateQuestionDto question, Guid id);
        Task<ServiceResponse<List<GetQuestionDto>>> DeleteQuestion(Guid id);
    }
}
