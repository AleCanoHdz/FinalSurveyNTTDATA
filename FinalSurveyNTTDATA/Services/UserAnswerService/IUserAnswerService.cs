using FinalSurveyNTTDATA.DTOs.QuestionAnswer;
using FinalSurveyNTTDATA.DTOs.UserAnswer;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Services.UserAnswerService
{
    public interface IUserAnswerService
    {
        Task<ServiceResponse<List<GetUserAnswerDto>>> GetUserAnswers();
        Task<ServiceResponse<GetUserAnswerDto>> GetUserAnswer(Guid id);
        Task<ServiceResponse<List<GetUserAnswerDto>>> AddUserAnswer(AddUserAnswerDto userAnswer);
        Task<ServiceResponse<GetUserAnswerDto>> UpdateUserAnswer(UpdateUserAnswerDto userAnswer, Guid id);
        Task<ServiceResponse<List<GetUserAnswerDto>>> DeleteUserAnswer(Guid id);
    }
}
