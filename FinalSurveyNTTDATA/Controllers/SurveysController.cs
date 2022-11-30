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
using FinalSurveyNTTDATA.DTOs.Role;
using FinalSurveyNTTDATA.DTOs.Survey;
using Microsoft.AspNetCore.Authorization;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SurveysController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Surveys
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetSurveyDto>>>> GetSurvey()
        {
            var response = new ServiceResponse<IEnumerable<GetSurveyDto>>();

            var survey = await _context.Survey.Include(c => c.Category).ToListAsync();

            response.Data = survey.Select(c => _mapper.Map<GetSurveyDto>(c)).ToList();

            return Ok(response);
        }

        // GET: api/Surveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSurveyDto>>> GetSurvey(int id)
        {
            var response = new ServiceResponse<GetSurveyDto>();
            var survey = await _context.Survey.FirstOrDefaultAsync(c => c.IdSurvey == id);

            if (survey != null)
            {
                response.Data = _mapper.Map<GetSurveyDto>(survey);
            }
            else
            {
                response.Success = false;
                response.Message = "Survey not found";

                return NotFound(response);
            }

            return Ok(response);
        }

        // PUT: api/Surveys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSurveyDto>>> PutSurvey(UpdateSurveyDto survey, int id)
        {
            ServiceResponse<GetSurveyDto> response = new ServiceResponse<GetSurveyDto>();
            try
            {
                var sv = await _context.Survey.FindAsync(id);

                if (SurveyExists(id))
                {
                    _mapper.Map(survey, sv);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetSurveyDto>(sv);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Survey not found";
                }
            }
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        // POST: api/Surveys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost,Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetSurveyDto>>>> PostSurvey(AddSurveyDto survey)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetSurveyDto>>();

            Survey sv = _mapper.Map<Survey>(survey);

            _context.Survey.Add(sv);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Survey.Select(c => _mapper.Map<GetSurveyDto>(c)).ToListAsync();

            return Ok(serviceResponse);
        }

        // DELETE: api/Surveys/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> DeleteSurvey(int id)
        {
            ServiceResponse<IEnumerable<GetSurveyDto>> serviceResponse = new ServiceResponse<IEnumerable<GetSurveyDto>>();

            try
            {
                Survey sv = await _context.Survey.FirstOrDefaultAsync(c => c.IdSurvey == id);

                if (sv != null)
                {
                    _context.Survey.Remove(sv);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Survey.Select(c => _mapper.Map<GetSurveyDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Survey not found";

                    return NotFound(serviceResponse);
                }
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return Ok(serviceResponse);
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.IdSurvey == id);
        }
    }
}
