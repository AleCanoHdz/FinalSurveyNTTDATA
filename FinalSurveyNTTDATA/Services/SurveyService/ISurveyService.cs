using FinalSurveyNTTDATA.DTOs.Role;
using FinalSurveyNTTDATA.DTOs.Survey;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Services.SurveyService
{
    public interface ISurveyService
    {
        Task<ServiceResponse<List<GetSurveyDto>>> GetSurveys();
        Task<ServiceResponse<GetSurveyDto>> GetSurvey(int id);
        Task<ServiceResponse<List<GetSurveyDto>>> AddSurvey(AddSurveyDto survey);
        Task<ServiceResponse<GetSurveyDto>> UpdateSurvey(UpdateSurveyDto survey, int id);
        Task<ServiceResponse<List<GetSurveyDto>>> DeleteSurvey(int id);
    }
}
