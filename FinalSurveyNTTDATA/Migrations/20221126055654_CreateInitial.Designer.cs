﻿// <auto-generated />
using System;
using FinalSurveyNTTDATA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinalSurveyNTTDATA.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221126055654_CreateInitial")]
    partial class CreateInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Category", b =>
                {
                    b.Property<Guid>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdCategory");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Question", b =>
                {
                    b.Property<Guid>("IdQuestion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("QuestionType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("QuestonTxt")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.HasKey("IdQuestion");

                    b.HasIndex("SurveyId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.QuestionAnswer", b =>
                {
                    b.Property<Guid>("IdQuestionAnswer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnswerOption")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("Correct")
                        .HasColumnType("bit");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IdQuestionAnswer");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionAnswer");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Role", b =>
                {
                    b.Property<Guid>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("IdRole");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Survey", b =>
                {
                    b.Property<int>("IdSurvey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSurvey"));

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("date");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IdSurvey");

                    b.HasIndex("CategoryId");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("FirstSurname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastSurname")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IdUser");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.UserAnswer", b =>
                {
                    b.Property<Guid>("IdUserAnswer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserAns")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("IdUserAnswer");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswer");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesIdRole")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UsersIdUser")
                        .HasColumnType("int");

                    b.HasKey("RolesIdRole", "UsersIdUser");

                    b.HasIndex("UsersIdUser");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Question", b =>
                {
                    b.HasOne("FinalSurveyNTTDATA.Models.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.QuestionAnswer", b =>
                {
                    b.HasOne("FinalSurveyNTTDATA.Models.Question", "Question")
                        .WithMany("QuestionAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Survey", b =>
                {
                    b.HasOne("FinalSurveyNTTDATA.Models.Category", "Category")
                        .WithMany("Surveys")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.UserAnswer", b =>
                {
                    b.HasOne("FinalSurveyNTTDATA.Models.Question", "Question")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalSurveyNTTDATA.Models.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("FinalSurveyNTTDATA.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesIdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalSurveyNTTDATA.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersIdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Category", b =>
                {
                    b.Navigation("Surveys");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Question", b =>
                {
                    b.Navigation("QuestionAnswers");

                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.Survey", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("FinalSurveyNTTDATA.Models.User", b =>
                {
                    b.Navigation("UserAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
