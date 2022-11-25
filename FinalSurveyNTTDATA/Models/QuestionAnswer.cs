using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Models;

[Table("QuestionAnswer")]
public partial class QuestionAnswer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid IdQuestionAnswer { get; set; }

    [Unicode(false)]
    public string AnswerOption { get; set; } = null!;

    public bool Correct { get; set; }

    public bool Status { get; set; }

    public Guid QuestionId { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("QuestionAnswers")]
    public virtual Question Question { get; set; } = null!;
}
