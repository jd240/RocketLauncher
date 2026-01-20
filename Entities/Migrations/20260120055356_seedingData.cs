using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "ModuleId", "Description", "IsActive", "Key", "SortOrder", "Title" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Tell us a bit about your business so we can build your personalised roadmap.", true, "intake", 0, "Initial Intake" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "HelpText", "IsActive", "IsRequired", "Key", "ModuleId", "Prompt", "QuestionType", "SortOrder", "Version" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222221"), null, true, true, "has_business_name", new Guid("11111111-1111-1111-1111-111111111111"), "Have you registered a business name?", "YesNo", 1, 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, true, true, "has_domain", new Guid("11111111-1111-1111-1111-111111111111"), "Have you registered a domain name?", "YesNo", 2, 1 },
                    { new Guid("22222222-2222-2222-2222-222222222223"), null, true, true, "has_business_email", new Guid("11111111-1111-1111-1111-111111111111"), "Have you set up business email?", "YesNo", 3, 1 },
                    { new Guid("22222222-2222-2222-2222-222222222224"), null, true, true, "has_business_plan", new Guid("11111111-1111-1111-1111-111111111111"), "Have you documented a business plan?", "YesNo", 4, 1 },
                    { new Guid("22222222-2222-2222-2222-222222222225"), null, true, true, "has_marketing_plan", new Guid("11111111-1111-1111-1111-111111111111"), "Have you documented a marketing plan?", "YesNo", 5, 1 },
                    { new Guid("22222222-2222-2222-2222-222222222226"), null, true, true, "has_social_accounts", new Guid("11111111-1111-1111-1111-111111111111"), "Have you set up any social media accounts?", "YesNo", 6, 1 }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "QuestionOptionId", "IsActive", "Label", "QuestionId", "SortOrder", "Value" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444441"), true, "Yes", new Guid("22222222-2222-2222-2222-222222222221"), 1, "yes" },
                    { new Guid("44444444-4444-4444-4444-444444444442"), true, "No", new Guid("22222222-2222-2222-2222-222222222221"), 2, "no" },
                    { new Guid("44444444-4444-4444-4444-444444444443"), true, "Yes", new Guid("22222222-2222-2222-2222-222222222222"), 1, "yes" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), true, "No", new Guid("22222222-2222-2222-2222-222222222222"), 2, "no" },
                    { new Guid("44444444-4444-4444-4444-444444444445"), true, "Yes", new Guid("22222222-2222-2222-2222-222222222223"), 1, "yes" },
                    { new Guid("44444444-4444-4444-4444-444444444446"), true, "No", new Guid("22222222-2222-2222-2222-222222222223"), 2, "no" },
                    { new Guid("44444444-4444-4444-4444-444444444447"), true, "Yes", new Guid("22222222-2222-2222-2222-222222222224"), 1, "yes" },
                    { new Guid("44444444-4444-4444-4444-444444444448"), true, "No", new Guid("22222222-2222-2222-2222-222222222224"), 2, "no" },
                    { new Guid("44444444-4444-4444-4444-444444444449"), true, "Yes", new Guid("22222222-2222-2222-2222-222222222225"), 1, "yes" },
                    { new Guid("44444444-4444-4444-4444-444444444450"), true, "No", new Guid("22222222-2222-2222-2222-222222222225"), 2, "no" },
                    { new Guid("44444444-4444-4444-4444-444444444451"), true, "Yes", new Guid("22222222-2222-2222-2222-222222222226"), 1, "yes" },
                    { new Guid("44444444-4444-4444-4444-444444444452"), true, "No", new Guid("22222222-2222-2222-2222-222222222226"), 2, "no" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444445"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444446"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444447"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444448"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444449"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444450"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444451"));

            migrationBuilder.DeleteData(
                table: "QuestionOptions",
                keyColumn: "QuestionOptionId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444452"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222225"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222226"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "ModuleId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
