using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Models;

[Table("Question")]
public partial class Question
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid IdQuestion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string QuestionTxt { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string QuestionType { get; set; } = null!;

    public int SurveyId { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<QuestionAnswer> QuestionAnswers { get; } = new List<QuestionAnswer>();

    [ForeignKey("SurveyId")]
    [InverseProperty("Questions")]
    public virtual Survey Survey { get; set; } = null!;

    [InverseProperty("Question")]
    public virtual ICollection<UserAnswer> UserAnswers { get; } = new List<UserAnswer>();
}
