using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.Models;
using AutoMapper;
using FinalSurveyNTTDATA.DTOs.Category;
using FinalSurveyNTTDATA.Services.CategoryService;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoriesController(DataContext context, IMapper mapper, ICategoryService categoryService)
        {
            _context = context;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCategoryDto>>>> GetCategories()
        {
            return Ok(await _categoryService.GetCategories());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> GetCategory(Guid id)
        {
            return Ok(await _categoryService.GetCategory(id));
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> PutCategory(UpdateCategoryDto update, Guid id)
        {
            var response = await _categoryService.UpdateCategory(update, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> AddCategory(AddCategoryDto category)
        {
            return Ok(await _categoryService.AddCategory(category));
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> DeleteCategory(Guid id)
        {
            var response = await _categoryService.DeleteCategory(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.IdCategory == id);
        }
    }
}
