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
using FinalSurveyNTTDATA.DTOs.Question;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using FinalSurveyNTTDATA.Services.QuestionService;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;

        public QuestionsController(DataContext context, IMapper mapper, IQuestionService questionService)
        {
            _context = context;
            _mapper = mapper;
            _questionService = questionService;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetQuestionDto>>>> GetQuestions()
        {
            return Ok(await _questionService.GetQuestions());
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> GetQuestion(Guid id)
        {
            return Ok(await _questionService.GetQuestion(id));
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> PutQuestion(UpdateQuestionDto update, Guid id)
        {
            var response = await _questionService.UpdateQuestion(update, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> AddQuestion(AddQuestionDto question)
        {
            return Ok(await _questionService.AddQuestion(question));
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> DeleteQuestion(Guid id)
        {
            var response = await _questionService.DeleteQuestion(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        private bool QuestionExists(Guid id)
        {
            return _context.Question.Any(e => e.IdQuestion == id);
        }
    }
}
