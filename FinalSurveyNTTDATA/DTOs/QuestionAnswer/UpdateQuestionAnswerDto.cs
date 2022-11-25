namespace FinalSurveyNTTDATA.DTOs.QuestionAnswer
{
    public class UpdateQuestionAnswerDto
    {
        public string AnswerOption { get; set; } = null!;

        public bool Correct { get; set; }

        public bool Status { get; set; }

        public Guid QuestionId { get; set; }
    }
}
