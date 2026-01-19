using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class UserDbcontext : DbContext
    {
        public UserDbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WhiteLabelTenant> WhiteLabelTenants { get; set; }

        public DbSet<Module> Modules { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserModule> UserModules { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tables
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<WhiteLabelTenant>().ToTable("WhiteLabelTenants");
            modelBuilder.Entity<Module>().ToTable("Modules");
            modelBuilder.Entity<TaskItem>().ToTable("TaskItems");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<QuestionOption>().ToTable("QuestionOptions");
            modelBuilder.Entity<UserAnswer>().ToTable("UserAnswers");
            modelBuilder.Entity<UserModule>().ToTable("UserModules");
            modelBuilder.Entity<UserTask>().ToTable("UserTasks");

            // Tenant
            modelBuilder.Entity<WhiteLabelTenant>()
                .HasIndex(t => t.Slug)
                .IsUnique();

            // If your User has TenantID FK + Tenant navigation, configure one-tenant-many-users
            modelBuilder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantID)
                .OnDelete(DeleteBehavior.Restrict);

            // Module
            modelBuilder.Entity<Module>()
                .HasIndex(m => m.Key)
                .IsUnique();

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Module)
                .WithMany(m => m.Tasks)
                .HasForeignKey(t => t.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question
            modelBuilder.Entity<Question>()
                .HasIndex(q => q.Key)
                .IsUnique();

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Module)
                .WithMany(m => m.Questions)
                .HasForeignKey(q => q.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionOption>()
                .HasOne(o => o.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional but recommended: prevent duplicate option values per question
            modelBuilder.Entity<QuestionOption>()
                .HasIndex(o => new { o.QuestionId, o.Value })
                .IsUnique();

            // UserAnswer (one answer per user per question)
            modelBuilder.Entity<UserAnswer>()
                .HasOne(a => a.User)
                .WithMany() // add User.Answers navigation later if you want
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(a => a.Question)
                .WithMany() // add Question.Answers navigation later if you want
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnswer>()
                .HasIndex(a => new { a.UserId, a.QuestionId })
                .IsUnique();

            // UserModule (one module row per user)
            modelBuilder.Entity<UserModule>()
                .HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserModule>()
                .HasOne(um => um.Module)
                .WithMany()
                .HasForeignKey(um => um.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserModule>()
                .HasIndex(um => new { um.UserId, um.ModuleId })
                .IsUnique();

            // UserTask (one task row per user per task item)
            modelBuilder.Entity<UserTask>()
                .HasOne(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTask>()
                .HasOne(ut => ut.TaskItem)
                .WithMany()
                .HasForeignKey(ut => ut.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTask>()
                .HasIndex(ut => new { ut.UserId, ut.TaskItemId })
                .IsUnique();
        }
    }
}