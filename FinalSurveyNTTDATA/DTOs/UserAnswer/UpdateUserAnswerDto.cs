namespace FinalSurveyNTTDATA.DTOs.UserAnswer
{
    public class UpdateUserAnswerDto
    {
        public string UserAns { get; set; } = null!;

        public int UserId { get; set; }

        public Guid QuestionId { get; set; }
    }
}
