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
using FinalSurveyNTTDATA.DTOs.QuestionAnswer;
using FinalSurveyNTTDATA.Services.QuestionAnswerService;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IQuestionAnswerService _questionAnswerService;

        public QuestionAnswersController(DataContext context, IMapper mapper, IQuestionAnswerService questionAnswerService)
        {
            _context = context;
            _mapper = mapper;
            _questionAnswerService = questionAnswerService;
        }

        // GET: api/QuestionAnswers
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionAnswerDto>>>> GetQuestionAnswers()
        {
            return Ok(await _questionAnswerService.GetQuestionAnswers());
        }

        // GET: api/QuestionAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionAnswerDto>>> GetQuestionAnswer(Guid id)
        {
            return Ok(await _questionAnswerService.GetQuestionAnswer(id));
        }

        // PUT: api/QuestionAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionAnswerDto>>> PutQuestionAnswer(UpdateQuestionAnswerDto update, Guid id)
        {
            var response = await _questionAnswerService.UpdateQuestionAnswer(update, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/QuestionAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetQuestionAnswerDto>>> AddQuestionAnswer(AddQuestionAnswerDto questionAnswer)
        {
            return Ok(await _questionAnswerService.AddQuestionAnswer(questionAnswer));
        }

        // DELETE: api/QuestionAnswers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionAnswerDto>>> DeleteQuestionAnswer(Guid id)
        {
            var response = await _questionAnswerService.DeleteQuestionAnswer(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        private bool QuestionAnswerExists(Guid id)
        {
            return _context.QuestionAnswer.Any(e => e.IdQuestionAnswer == id);
        }
    }
}
