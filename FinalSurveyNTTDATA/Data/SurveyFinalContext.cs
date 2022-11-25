using System;
using System.Collections.Generic;
using FinalSurveyNTTDATA.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalSurveyNTTDATA.Data;

public partial class SurveyFinalContext : DbContext
{
    public SurveyFinalContext(DbContextOptions<SurveyFinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAnswer> UserAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.IdCategory).ValueGeneratedNever();
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.IdQuestion).ValueGeneratedNever();

            entity.HasOne(d => d.Survey).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Survey");
        });

        modelBuilder.Entity<QuestionAnswer>(entity =>
        {
            entity.Property(e => e.IdQuestionAnswer).ValueGeneratedNever();

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionAnswer_Question");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.IdRole).ValueGeneratedNever();
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Surveys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Survey_Category");
        });

        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.Property(e => e.IdUserAnswer).ValueGeneratedNever();

            entity.HasOne(d => d.Question).WithMany(p => p.UserAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAnswer_Question");

            entity.HasOne(d => d.User).WithMany(p => p.UserAnswers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAnswer_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
