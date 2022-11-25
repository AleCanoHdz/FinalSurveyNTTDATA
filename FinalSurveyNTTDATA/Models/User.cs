using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int IdUser { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string FirstSurname { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? LastSurname { get; set; }

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public bool? Status { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserAnswer> UserAnswers { get; } = new List<UserAnswer>();
}
