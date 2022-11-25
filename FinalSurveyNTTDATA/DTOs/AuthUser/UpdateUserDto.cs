namespace FinalSurveyNTTDATA.DTOs.AuthUser
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = null!;

        public string FirstSurname { get; set; } = null!;

        public string? LastSurname { get; set; }
        public string Password { get; set; } = string.Empty;

        public byte[]? Photo { get; set; }

        public bool? Status { get; set; }
    }
}
