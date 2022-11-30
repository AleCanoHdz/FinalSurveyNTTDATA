using FinalSurveyNTTDATA.DTOs.AuthUser;
using FinalSurveyNTTDATA.DTOs.Question;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.DTOs.UserAnswer
{
    public class GetUserAnswerDto
    {
        public Guid IdUserAnswer { get; set; }

        public string UserAns { get; set; } = null!;

        public GetUserDto? User { get; set; }

        public GetQuestionDto? Question { get; set; }
    }
}
