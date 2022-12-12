using AutoMapper;
using FinalSurveyNTTDATA.Data;
using FinalSurveyNTTDATA.DTOs.Survey;
using FinalSurveyNTTDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Services.SurveyService
{
    public class SurveyService : ISurveyService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SurveyService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetSurveyDto>>> AddSurvey(AddSurveyDto survey)
        {
            var serviceResponse = new ServiceResponse<List<GetSurveyDto>>();

            Survey sv = _mapper.Map<Survey>(survey);

            _context.Survey.Add(sv);

            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Survey.Select(c => _mapper.Map<GetSurveyDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSurveyDto>>> DeleteSurvey(int id)
        {
            ServiceResponse<List<GetSurveyDto>> serviceResponse = new ServiceResponse<List<GetSurveyDto>>();

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

        public async Task<ServiceResponse<List<GetSurveyDto>>> GetSurveys()
        {
            var response = new ServiceResponse<List<GetSurveyDto>>();

            var survey = await _context.Survey.Include(c => c.Category).ToListAsync();

            response.Data = survey.Select(c => _mapper.Map<GetSurveyDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetSurveyDto>> GetSurvey(int id)
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

                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<GetSurveyDto>> UpdateSurvey(UpdateSurveyDto survey, int id)
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
                return response;
            }

            return response;
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.IdSurvey == id);
        }
    }
}
