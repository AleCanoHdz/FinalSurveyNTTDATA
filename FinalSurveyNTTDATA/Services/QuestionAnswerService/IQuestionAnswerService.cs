using FinalSurveyNTTDATA.DTOs.Category;
using FinalSurveyNTTDATA.DTOs.QuestionAnswer;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Services.QuestionAnswerService
{
    public interface IQuestionAnswerService
    {
        Task<ServiceResponse<List<GetQuestionAnswerDto>>> GetQuestionAnswers();
        Task<ServiceResponse<GetQuestionAnswerDto>> GetQuestionAnswer(Guid id);
        Task<ServiceResponse<List<GetQuestionAnswerDto>>> AddQuestionAnswer(AddQuestionAnswerDto questionAnswer);
        Task<ServiceResponse<GetQuestionAnswerDto>> UpdateQuestionAnswer(UpdateQuestionAnswerDto questionAnswer, Guid id);
        Task<ServiceResponse<List<GetQuestionAnswerDto>>> DeleteQuestionAnswer(Guid id);
    }
}
