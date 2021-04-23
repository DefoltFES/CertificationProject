using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Npgsql;

#nullable disable

namespace CertificationProject.Models {
    public partial class TestContext : DbContext {
        private readonly string _connectionString;

        public TestContext(string connectionString) {
            _connectionString = connectionString;
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options) {
        }

        static TestContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<User.RoleState>("user_role");

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersAndTests> Usersandtests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasPostgresEnum(null, "user_role", new[] {"user", "admin"})
                .HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Answer>(entity => {
                entity.ToTable("answers");
                
                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.UserAnswer)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("useranswer");

                entity.HasOne(d => d.Question)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answers_question_id_fkey");

                entity.HasOne(d => d.Session)
                    .WithMany()
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("answers_session_id_fkey");
            });

            modelBuilder.Entity<Question>(entity => {
                entity.ToTable("questions");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Answer)
                    .HasColumnType("character varying")
                    .HasColumnName("answer");

                entity.Property(e => e.QuestionString)
                    .HasColumnType("character varying")
                    .HasColumnName("question");

                entity.Property(e => e.Topic)
                    .HasColumnType("character varying")
                    .HasColumnName("theme");
            });

            modelBuilder.Entity<Result>(entity => {
                entity.HasKey(e => e.SessionId)
                    .HasName("results_pkey");

                entity.ToTable("results");

                entity.Property(e => e.SessionId)
                    .ValueGeneratedNever()
                    .HasColumnName("session_id");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("date")
                    .HasColumnName("date_end");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("date_start");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("results_user_id_fkey");
            });

            modelBuilder.Entity<Test>(entity => {
                entity.ToTable("tests");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("theme");
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnType("user_role")
                    .HasColumnName("role");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("password");
            });

            modelBuilder.Entity<UsersAndTests>(entity => {
                entity.ToTable("usersandtest");
                
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Test)
                    .WithMany()
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("usersandtest_test_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("usersandtest_user_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}