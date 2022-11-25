using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Models;

[Table("UserAnswer")]
public partial class UserAnswer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public Guid IdUserAnswer { get; set; }

    [Unicode(false)]
    public string UserAns { get; set; } = null!;

    public int UserId { get; set; }

    public Guid QuestionId { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("UserAnswers")]
    public virtual Question Question { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserAnswers")]
    public virtual User User { get; set; } = null!;
}
