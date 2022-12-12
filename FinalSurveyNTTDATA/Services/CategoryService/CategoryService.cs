using AutoMapper;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.DTOs.Category;
using FinalSurveyNTTDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CategoryService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto category)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

            Category cat = _mapper.Map<Category>(category);

            _context.Category.Add(cat);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Category.Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(Guid id)
        {
            ServiceResponse<List<GetCategoryDto>> serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

            try
            {
                Category cat = await _context.Category.FirstOrDefaultAsync(c => c.IdCategory.ToString().ToUpper() == id.ToString().ToUpper());

                if (cat != null)
                {
                    _context.Category.Remove(cat);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Category.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Category not found";

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

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetCategories()
        {
            var response = new ServiceResponse<List<GetCategoryDto>>();

            var category = await _context.Category.ToListAsync();

            response.Data = category.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategory(Guid id)
        {
            var response = new ServiceResponse<GetCategoryDto>();
            var cat = await _context.Category.FirstOrDefaultAsync(c => c.IdCategory.ToString().ToUpper() == id.ToString().ToUpper());

            if (cat != null)
            {
                response.Data = _mapper.Map<GetCategoryDto>(cat);
            }
            else
            {
                response.Success = false;
                response.Message = "Category not found";

                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto category, Guid id)
        {
            ServiceResponse<GetCategoryDto> response = new ServiceResponse<GetCategoryDto>();
            try
            {
                var cat = await _context.Category.FindAsync(id);

                if (CategoryExists(id))
                {
                    _mapper.Map(category, cat);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetCategoryDto>(cat);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Category not found";
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

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.IdCategory == id);
        }
    }
}
