using FinalSurveyNTTDATA.DTOs.Category;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<GetCategoryDto>>> GetCategories();
        Task<ServiceResponse<GetCategoryDto>> GetCategory(Guid id);
        Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto category);
        Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto category, Guid id);
        Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(Guid id);
    }
}
