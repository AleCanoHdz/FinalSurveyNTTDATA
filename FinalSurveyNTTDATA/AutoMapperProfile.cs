using AutoMapper;
using FinalSurveyNTTDATA.DTOs.Category;
using FinalSurveyNTTDATA.DTOs.Question;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Category mapping
            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Category>();  
            CreateMap<UpdateCategoryDto, Category>();

            //Question mapping
            CreateMap<Question, GetQuestionDto>();
            CreateMap<AddQuestionDto, Question>();
            CreateMap<UpdateQuestionDto, Question>();
        }
    }
}
