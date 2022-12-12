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
using FinalSurveyNTTDATA.DTOs.UserAnswer;
using FinalSurveyNTTDATA.Services.UserAnswerService;

namespace FinalSurveyNTTDATA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnswersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAnswerService _userAnswerService;

        public UserAnswersController(DataContext context, IMapper mapper, IUserAnswerService userAnswerService)
        {
            _context = context;
            _mapper = mapper;
            _userAnswerService = userAnswerService;
        }

        // GET: api/UserAnswers
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetUserAnswerDto>>>> GetUserAnswers()
        {
            return Ok(await _userAnswerService.GetUserAnswers());
        }

        // GET: api/UserAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> GetUserAnswer(Guid id)
        {
            return Ok(await _userAnswerService.GetUserAnswer(id));
        }

        // PUT: api/UserAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> PutUserAnswer(UpdateUserAnswerDto update, Guid id)
        {
            var response = await _userAnswerService.UpdateUserAnswer(update, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/UserAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> AddUserAnswer(AddUserAnswerDto ua)
        {
            return Ok(await _userAnswerService.AddUserAnswer(ua));
        }

        // DELETE: api/UserAnswers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserAnswerDto>>> DeleteUserAnswer(Guid id)
        {
            var response = await _userAnswerService.DeleteUserAnswer(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        private bool UserAnswerExists(Guid id)
        {
            return _context.UserAnswer.Any(e => e.IdUserAnswer == id);
        }
    }
}
