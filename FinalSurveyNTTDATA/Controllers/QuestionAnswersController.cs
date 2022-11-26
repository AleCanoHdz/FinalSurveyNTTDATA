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

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionAnswersController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/QuestionAnswers
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionAnswerDto>>>> GetQuestionAnswer()
        {
            var response = new ServiceResponse<IEnumerable<GetQuestionAnswerDto>>();

            var qa = await _context.QuestionAnswer.Include(q => q.Question).ToListAsync();

            response.Data = qa.Select(c => _mapper.Map<GetQuestionAnswerDto> (c)).ToList();

            return Ok(response);
        }

        // GET: api/QuestionAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionAnswerDto>>> GetQuestionAnswer(Guid id)
        {
            var response = new ServiceResponse<GetQuestionAnswerDto>();
            var qa = await _context.QuestionAnswer.FirstOrDefaultAsync(c => c.IdQuestionAnswer.ToString().ToUpper() == id.ToString().ToUpper());

            if (qa != null)
            {
                response.Data = _mapper.Map<GetQuestionAnswerDto>(qa);
            }
            else
            {
                response.Success = false;
                response.Message = "Question Answer not found";

                return NotFound(response);
            }

            return Ok(response);
        }

        // PUT: api/QuestionAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionAnswerDto>>> PutQuestionAnswer(UpdateQuestionAnswerDto qa, Guid id)
        {
            ServiceResponse<GetQuestionAnswerDto> response = new ServiceResponse<GetQuestionAnswerDto>();
            try
            {
                var questa = await _context.QuestionAnswer.FindAsync(id);

                if (QuestionAnswerExists(id))
                {
                    _mapper.Map(qa, questa);

                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetQuestionAnswerDto>(questa);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Question answer not found";
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

        // POST: api/QuestionAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetQuestionAnswerDto>>>> PostQuestionAnswer(AddQuestionAnswerDto qa)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetQuestionAnswerDto>>();

            QuestionAnswer questa = _mapper.Map<QuestionAnswer>(qa);

            _context.QuestionAnswer.Add(questa);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.QuestionAnswer.Select(c => _mapper.Map<GetQuestionAnswerDto>(c)).ToListAsync();

            return Ok(serviceResponse);
        }

        // DELETE: api/QuestionAnswers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetQuestionAnswerDto>>> DeleteQuestionAnswer(Guid id)
        {
            ServiceResponse<IEnumerable<GetQuestionAnswerDto>> serviceResponse = new ServiceResponse<IEnumerable<GetQuestionAnswerDto>>();

            try
            {
                QuestionAnswer quest = await _context.QuestionAnswer.FirstOrDefaultAsync(c => c.IdQuestionAnswer.ToString().ToUpper() == id.ToString().ToUpper());

                if (quest != null)
                {
                    _context.QuestionAnswer.Remove(quest);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.QuestionAnswer.Select(c => _mapper.Map<GetQuestionAnswerDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Question answer not found";

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

        private bool QuestionAnswerExists(Guid id)
        {
            return _context.QuestionAnswer.Any(e => e.IdQuestionAnswer == id);
        }
    }
}
