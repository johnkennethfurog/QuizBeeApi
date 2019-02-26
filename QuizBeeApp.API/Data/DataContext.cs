using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<JudgeVerdict> JudgeVerdicts { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantAnswer> ParticipantAnswers { get; set; }
        public DbSet<QuestionChoice> QuestionChoices { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<QuizItem> QuizItems { get; set; }
    }
}