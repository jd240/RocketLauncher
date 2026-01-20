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
            base.OnModelCreating(modelBuilder);

            // -------------------------
            // Intake Module Seed
            // -------------------------
            var intakeModuleId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            modelBuilder.Entity<Module>().HasData(
                new Module
                {
                    ModuleId = intakeModuleId,
                    Key = "intake",
                    Title = "Initial Intake",
                    Description = "Tell us a bit about your business so we can build your personalised roadmap.",
                    SortOrder = 0,
                    IsActive = true
                }
            );

            // -------------------------
            // Intake Questions Seed
            // -------------------------
            var q1Id = Guid.Parse("22222222-2222-2222-2222-222222222221");
            var q2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var q3Id = Guid.Parse("22222222-2222-2222-2222-222222222223");
            var q4Id = Guid.Parse("22222222-2222-2222-2222-222222222224");
            var q5Id = Guid.Parse("22222222-2222-2222-2222-222222222225");
            var q6Id = Guid.Parse("22222222-2222-2222-2222-222222222226");

            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    QuestionId = q1Id,
                    ModuleId = intakeModuleId,
                    Key = "has_business_name",
                    Prompt = "Have you registered a business name?",
                    QuestionType = "YesNo",
                    IsRequired = true,
                    SortOrder = 1,
                    IsActive = true,
                    Version = 1
                },
                new Question
                {
                    QuestionId = q2Id,
                    ModuleId = intakeModuleId,
                    Key = "has_domain",
                    Prompt = "Have you registered a domain name?",
                    QuestionType = "YesNo",
                    IsRequired = true,
                    SortOrder = 2,
                    IsActive = true,
                    Version = 1
                },
                new Question
                {
                    QuestionId = q3Id,
                    ModuleId = intakeModuleId,
                    Key = "has_business_email",
                    Prompt = "Have you set up business email?",
                    QuestionType = "YesNo",
                    IsRequired = true,
                    SortOrder = 3,
                    IsActive = true,
                    Version = 1
                },
                new Question
                {
                    QuestionId = q4Id,
                    ModuleId = intakeModuleId,
                    Key = "has_business_plan",
                    Prompt = "Have you documented a business plan?",
                    QuestionType = "YesNo",
                    IsRequired = true,
                    SortOrder = 4,
                    IsActive = true,
                    Version = 1
                },
                new Question
                {
                    QuestionId = q5Id,
                    ModuleId = intakeModuleId,
                    Key = "has_marketing_plan",
                    Prompt = "Have you documented a marketing plan?",
                    QuestionType = "YesNo",
                    IsRequired = true,
                    SortOrder = 5,
                    IsActive = true,
                    Version = 1
                },
                new Question
                {
                    QuestionId = q6Id,
                    ModuleId = intakeModuleId,
                    Key = "has_social_accounts",
                    Prompt = "Have you set up any social media accounts?",
                    QuestionType = "YesNo",
                    IsRequired = true,
                    SortOrder = 6,
                    IsActive = true,
                    Version = 1
                }
            );

            // -------------------------
            // Yes/No Options Seed
            // -------------------------
            var yesOptionId = Guid.Parse("33333333-3333-3333-3333-333333333331");
            var noOptionId = Guid.Parse("33333333-3333-3333-3333-333333333332");

            modelBuilder.Entity<QuestionOption>().HasData(
                // Q1
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444441"), QuestionId = q1Id, Value = "yes", Label = "Yes", SortOrder = 1, IsActive = true },
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444442"), QuestionId = q1Id, Value = "no", Label = "No", SortOrder = 2, IsActive = true },

                // Q2
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444443"), QuestionId = q2Id, Value = "yes", Label = "Yes", SortOrder = 1, IsActive = true },
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444444"), QuestionId = q2Id, Value = "no", Label = "No", SortOrder = 2, IsActive = true },

                // Q3
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444445"), QuestionId = q3Id, Value = "yes", Label = "Yes", SortOrder = 1, IsActive = true },
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444446"), QuestionId = q3Id, Value = "no", Label = "No", SortOrder = 2, IsActive = true },

                // Q4
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444447"), QuestionId = q4Id, Value = "yes", Label = "Yes", SortOrder = 1, IsActive = true },
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444448"), QuestionId = q4Id, Value = "no", Label = "No", SortOrder = 2, IsActive = true },

                // Q5
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444449"), QuestionId = q5Id, Value = "yes", Label = "Yes", SortOrder = 1, IsActive = true },
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444450"), QuestionId = q5Id, Value = "no", Label = "No", SortOrder = 2, IsActive = true },

                // Q6
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444451"), QuestionId = q6Id, Value = "yes", Label = "Yes", SortOrder = 1, IsActive = true },
                new QuestionOption { QuestionOptionId = Guid.Parse("44444444-4444-4444-4444-444444444452"), QuestionId = q6Id, Value = "no", Label = "No", SortOrder = 2, IsActive = true }
            );
        }
    }
}