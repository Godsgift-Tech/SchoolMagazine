using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobCategorySubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "JobPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExperienceLevel",
                table: "JobPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "JobPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxSalary",
                table: "JobPosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinSalary",
                table: "JobPosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExperienceLevel",
                table: "JobAlerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobType",
                table: "JobAlerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxSalary",
                table: "JobAlerts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinSalary",
                table: "JobAlerts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NotificationFrequency",
                table: "JobAlerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "JobCategoryPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategoryPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobCategoryPreferences_JobAlerts_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "JobAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobCategoryPreferences_SubscriptionId",
                table: "JobCategoryPreferences",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobCategoryPreferences");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ExperienceLevel",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "MaxSalary",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "MinSalary",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ExperienceLevel",
                table: "JobAlerts");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "JobAlerts");

            migrationBuilder.DropColumn(
                name: "MaxSalary",
                table: "JobAlerts");

            migrationBuilder.DropColumn(
                name: "MinSalary",
                table: "JobAlerts");

            migrationBuilder.DropColumn(
                name: "NotificationFrequency",
                table: "JobAlerts");
        }
    }
}
