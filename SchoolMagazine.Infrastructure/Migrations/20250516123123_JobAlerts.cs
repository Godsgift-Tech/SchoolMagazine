using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobAlerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "JobPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "JobMessages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "JobMessages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "JobApplications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobAlerts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscribedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobAlerts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPosts_UserId",
                table: "JobPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMessages_UserId",
                table: "JobMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMessages_UserId1",
                table: "JobMessages",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_UserId1",
                table: "JobApplications",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_JobAlerts_UserId",
                table: "JobAlerts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_UserId1",
                table: "JobApplications",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobMessages_AspNetUsers_UserId",
                table: "JobMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobMessages_AspNetUsers_UserId1",
                table: "JobMessages",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_AspNetUsers_UserId",
                table: "JobPosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_UserId1",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobMessages_AspNetUsers_UserId",
                table: "JobMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_JobMessages_AspNetUsers_UserId1",
                table: "JobMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_AspNetUsers_UserId",
                table: "JobPosts");

            migrationBuilder.DropTable(
                name: "JobAlerts");

            migrationBuilder.DropIndex(
                name: "IX_JobPosts_UserId",
                table: "JobPosts");

            migrationBuilder.DropIndex(
                name: "IX_JobMessages_UserId",
                table: "JobMessages");

            migrationBuilder.DropIndex(
                name: "IX_JobMessages_UserId1",
                table: "JobMessages");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_UserId1",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobMessages");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "JobMessages");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "JobApplications");
        }
    }
}
