using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class rebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhiteLabelTenants_Users_UserID",
                table: "WhiteLabelTenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhiteLabelTenants",
                table: "WhiteLabelTenants");

            migrationBuilder.DropIndex(
                name: "IX_WhiteLabelTenants_TenantID",
                table: "WhiteLabelTenants");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "WhiteLabelTenants");

            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "WhiteLabelTenants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "WhiteLabelTenants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "WhiteLabelTenants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "WhiteLabelTenants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WhiteLabelTenants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "WhiteLabelTenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhiteLabelTenants",
                table: "WhiteLabelTenants",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_WhiteLabelTenants_Slug",
                table: "WhiteLabelTenants",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssociatedTenantID",
                table: "Users",
                column: "AssociatedTenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WhiteLabelTenants_AssociatedTenantID",
                table: "Users",
                column: "AssociatedTenantID",
                principalTable: "WhiteLabelTenants",
                principalColumn: "TenantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_WhiteLabelTenants_AssociatedTenantID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhiteLabelTenants",
                table: "WhiteLabelTenants");

            migrationBuilder.DropIndex(
                name: "IX_WhiteLabelTenants_Slug",
                table: "WhiteLabelTenants");

            migrationBuilder.DropIndex(
                name: "IX_Users_AssociatedTenantID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "WhiteLabelTenants");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "WhiteLabelTenants");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "WhiteLabelTenants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WhiteLabelTenants");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "WhiteLabelTenants");

            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "WhiteLabelTenants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "WhiteLabelTenants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhiteLabelTenants",
                table: "WhiteLabelTenants",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WhiteLabelTenants_TenantID",
                table: "WhiteLabelTenants",
                column: "TenantID",
                unique: true,
                filter: "[TenantID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_WhiteLabelTenants_Users_UserID",
                table: "WhiteLabelTenants",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
