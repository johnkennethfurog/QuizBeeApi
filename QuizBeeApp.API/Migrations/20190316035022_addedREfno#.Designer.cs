﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizBeeApp.API.Data;

namespace QuizBeeApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190316035022_addedREfno#")]
    partial class addedREfno
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuizBeeApp.API.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.Judge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress");

                    b.Property<int?>("EventId");

                    b.Property<bool>("IsHead");

                    b.Property<bool>("IsVerify");

                    b.Property<string>("Name");

                    b.Property<string>("RefNo");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Judges");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.JudgeVerdict", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<int?>("JudgeId");

                    b.Property<int?>("ParticipantAnswerId");

                    b.Property<string>("Remarks");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("JudgeId");

                    b.HasIndex("ParticipantAnswerId");

                    b.ToTable("JudgeVerdicts");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventId");

                    b.Property<bool>("IsVerify");

                    b.Property<string>("Name");

                    b.Property<string>("ReferenceNumber");

                    b.Property<double>("TotalScores");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.ParticipantAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<bool>("IsCorrect");

                    b.Property<int?>("ParticipantId");

                    b.Property<double>("PointsEarned");

                    b.Property<int?>("QuizItemId");

                    b.Property<bool>("RequestedForVerification");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("QuizItemId");

                    b.ToTable("ParticipantAnswers");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.QuestionCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DefaultTimeLimit");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("QuestionCategories");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.QuestionChoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Choice");

                    b.Property<int?>("QuizItemId");

                    b.HasKey("Id");

                    b.HasIndex("QuizItemId");

                    b.ToTable("QuestionChoices");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.QuizItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("EventId");

                    b.Property<double>("Point");

                    b.Property<string>("Question");

                    b.Property<int>("TimeLimit");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EventId");

                    b.ToTable("QuizItems");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.Judge", b =>
                {
                    b.HasOne("QuizBeeApp.API.Models.Event", "Event")
                        .WithMany("Judges")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.JudgeVerdict", b =>
                {
                    b.HasOne("QuizBeeApp.API.Models.Judge", "Judge")
                        .WithMany()
                        .HasForeignKey("JudgeId");

                    b.HasOne("QuizBeeApp.API.Models.ParticipantAnswer", "ParticipantAnswer")
                        .WithMany("JudgeVerdicts")
                        .HasForeignKey("ParticipantAnswerId");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.Participant", b =>
                {
                    b.HasOne("QuizBeeApp.API.Models.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.ParticipantAnswer", b =>
                {
                    b.HasOne("QuizBeeApp.API.Models.Participant", "Participant")
                        .WithMany("ParticipantAnswers")
                        .HasForeignKey("ParticipantId");

                    b.HasOne("QuizBeeApp.API.Models.QuizItem", "QuizItem")
                        .WithMany("ParticipantAnswers")
                        .HasForeignKey("QuizItemId");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.QuestionChoice", b =>
                {
                    b.HasOne("QuizBeeApp.API.Models.QuizItem", "QuizItem")
                        .WithMany("QuestionChoices")
                        .HasForeignKey("QuizItemId");
                });

            modelBuilder.Entity("QuizBeeApp.API.Models.QuizItem", b =>
                {
                    b.HasOne("QuizBeeApp.API.Models.QuestionCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("QuizBeeApp.API.Models.Event", "Event")
                        .WithMany("QuizItems")
                        .HasForeignKey("EventId");
                });
#pragma warning restore 612, 618
        }
    }
}
