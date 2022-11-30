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

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionDto>>>> GetQuestion()
        {
            var response = new ServiceResponse<IEnumerable<GetQuestionDto>>();

            var question = await _context.Question.Include(s => s.Survey).ToListAsync();

            response.Data = question.Select(c => _mapper.Map<GetQuestionDto>(c)).ToList();

            return Ok(response);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> GetQuestion(Guid id)
        {
            var response = new ServiceResponse<GetQuestionDto>();
            var quest = await _context.Question.FirstOrDefaultAsync(c => c.IdQuestion.ToString().ToUpper() == id.ToString().ToUpper());

            if (quest != null)
            {
                response.Data = _mapper.Map<GetQuestionDto>(quest);
            }
            else
            {
                response.Success = false;
                response.Message = "Question not found";

                return NotFound(response);
            }

            return Ok(response);
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> PutQuestion(UpdateQuestionDto question, Guid id)
        {
            ServiceResponse<GetQuestionDto> response = new ServiceResponse<GetQuestionDto>();
            try
            {
                var quest = await _context.Question.FindAsync(id);

                if (QuestionExists(id))
                {
                    _mapper.Map(question, quest);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetQuestionDto>(quest);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Question not found";
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

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionDto>>>> PostQuestion(AddQuestionDto question)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetQuestionDto>>();

            Question quest = _mapper.Map<Question>(question);

            _context.Question.Add(quest);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Question.Select(c => _mapper.Map<GetQuestionDto>(c)).ToListAsync();

            return Ok(serviceResponse);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetQuestionDto>>> DeleteQuestion(Guid id)
        {
            ServiceResponse<IEnumerable<GetQuestionDto>> serviceResponse = new ServiceResponse<IEnumerable<GetQuestionDto>>();

            try
            {
                Question quest = await _context.Question.FirstOrDefaultAsync(c => c.IdQuestion.ToString().ToUpper() == id.ToString().ToUpper());

                if (quest != null)
                {
                    _context.Question.Remove(quest);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Question.Select(c => _mapper.Map<GetQuestionDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Question No Encontrado";

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

        private bool QuestionExists(Guid id)
        {
            return _context.Question.Any(e => e.IdQuestion == id);
        }
    }
}
