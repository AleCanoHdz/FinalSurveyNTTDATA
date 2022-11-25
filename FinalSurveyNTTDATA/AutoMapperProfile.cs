using AutoMapper;
using FinalSurveyNTTDATA.DTOs.AuthUser;
using FinalSurveyNTTDATA.DTOs.Category;
using FinalSurveyNTTDATA.DTOs.Question;
using FinalSurveyNTTDATA.DTOs.QuestionAnswer;
using FinalSurveyNTTDATA.DTOs.Role;
using FinalSurveyNTTDATA.DTOs.Survey;
using FinalSurveyNTTDATA.DTOs.UserAnswer;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User mapping
            CreateMap<User, GetUserDto>();

            //Category mapping
            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            //Question mapping
            CreateMap<Question, GetQuestionDto>();
            CreateMap<AddQuestionDto, Question>();
            CreateMap<UpdateQuestionDto, Question>();

            //QuestionAnswer mapping
            CreateMap<QuestionAnswer, GetQuestionAnswerDto>();
            CreateMap<AddQuestionAnswerDto, QuestionAnswer>();
            CreateMap<UpdateQuestionAnswerDto, QuestionAnswer>();

            //Role mapping
            CreateMap<Role, GetRoleDto>();
            CreateMap<AddRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            //Survey mapping
            CreateMap<Survey, GetSurveyDto>();
            CreateMap<AddSurveyDto, Survey>();
            CreateMap<UpdateSurveyDto, Survey>();

            //UserAnswer mapping
            CreateMap<UserAnswer, GetUserAnswerDto>();
            CreateMap<AddUserAnswerDto, UserAnswer>();
            CreateMap<UpdateUserAnswerDto, UserAnswer>();
        }
    }
}
