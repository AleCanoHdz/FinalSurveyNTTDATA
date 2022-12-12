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
using FinalSurveyNTTDATA.DTOs.Survey;
using Microsoft.AspNetCore.Authorization;
using FinalSurveyNTTDATA.Services.SurveyService;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ISurveyService _surveyService;

        public SurveysController(DataContext context, IMapper mapper, ISurveyService surveyService)
        {
            _context = context;
            _mapper = mapper;
            _surveyService = surveyService;
        }

        // GET: api/Surveys
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetSurveyDto>>>> GetSurveys()
        {
            return Ok(await _surveyService.GetSurveys());
        }

        // GET: api/Surveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSurveyDto>>> GetSurvey(int id)
        {
            return Ok(await _surveyService.GetSurvey(id));
        }

        // PUT: api/Surveys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSurveyDto>>> PutSurvey(UpdateSurveyDto update, int id)
        {
            var response = await _surveyService.UpdateSurvey(update, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Surveys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost,Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetSurveyDto>>> AddSurvey(AddSurveyDto survey)
        {
            return Ok(await _surveyService.AddSurvey(survey));
        }

        // DELETE: api/Surveys/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetSurveyDto>>> DeleteSurvey(int id)
        {
            var response = await _surveyService.DeleteSurvey(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.IdSurvey == id);
        }
    }
}
