namespace FinalSurveyNTTDATA.DTOs.Question
{
    public class AddQuestionDto
    {
        public string QuestonTxt { get; set; } = null!;
        public string QuestionType { get; set; } = null!;
        public int SurveyId { get; set; }
    }
}
