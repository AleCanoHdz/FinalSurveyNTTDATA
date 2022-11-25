using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Models;

[Table("Survey")]
public partial class Survey
{
    [Key]
    public int IdSurvey { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime RegisterDate { get; set; }

    public bool Status { get; set; }

    public Guid CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Surveys")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Survey")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
