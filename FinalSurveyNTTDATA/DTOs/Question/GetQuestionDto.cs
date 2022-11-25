using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinalSurveyNTTDATA.DTOs.Question
{
    public class GetQuestionDto
    {
        public Guid IdQuestion { get; set; }
        public string QuestonTxt { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public int SurveyId { get; set; }
    }
}
