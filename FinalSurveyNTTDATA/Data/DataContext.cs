using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalSurveyNTTDATA.Models;

namespace FinalSurveyNTTDATA.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<FinalSurveyNTTDATA.Models.User> User { get; set; } = default!;

        public DbSet<FinalSurveyNTTDATA.Models.Category> Category { get; set; }

        public DbSet<FinalSurveyNTTDATA.Models.Question> Question { get; set; }

        public DbSet<FinalSurveyNTTDATA.Models.QuestionAnswer> QuestionAnswer { get; set; }

        public DbSet<FinalSurveyNTTDATA.Models.Role> Role { get; set; }

        public DbSet<FinalSurveyNTTDATA.Models.Survey> Survey { get; set; }

        public DbSet<FinalSurveyNTTDATA.Models.UserAnswer> UserAnswer { get; set; }
    }
}
