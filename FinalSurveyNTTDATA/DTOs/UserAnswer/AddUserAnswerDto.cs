namespace FinalSurveyNTTDATA.DTOs.UserAnswer
{
    public class AddUserAnswerDto
    {
        public string UserAns { get; set; } = null!;

        public int UserId { get; set; }

        public Guid QuestionId { get; set; }
    }
}
