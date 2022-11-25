namespace FinalSurveyNTTDATA.DTOs.Survey
{
    public class UpdateSurveyDto
    {
        public string Name { get; set; } = null!;

        public DateTime RegisterDate { get; set; }

        public bool Status { get; set; }

        public Guid CategoryId { get; set; }
    }
}
