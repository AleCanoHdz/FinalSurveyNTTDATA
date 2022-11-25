using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinalSurveyNTTDATA.DTOs.Category
{
    public class GetCategoryDto
    {
        public Guid IdCategory { get; set; }
        public string Name { get; set; } = null!;
    }
}
