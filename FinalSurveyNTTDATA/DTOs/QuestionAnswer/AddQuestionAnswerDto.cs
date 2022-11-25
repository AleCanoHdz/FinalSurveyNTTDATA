namespace FinalSurveyNTTDATA.DTOs.QuestionAnswer
{
    public class AddQuestionAnswerDto
    {
        public string AnswerOption { get; set; } = null!;

        public bool Correct { get; set; }

        public bool Status { get; set; }

        public Guid QuestionId { get; set; }
    }
}
