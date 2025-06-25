using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SchoolRatingsUpgraded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SchoolRatings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RatingValue",
                table: "SchoolRatings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolAdminId",
                table: "SchoolRatings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId1",
                table: "SchoolRatings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolRatings_SchoolAdminId",
                table: "SchoolRatings",
                column: "SchoolAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolRatings_SchoolId1",
                table: "SchoolRatings",
                column: "SchoolId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolRatings_AspNetUsers_SchoolAdminId",
                table: "SchoolRatings",
                column: "SchoolAdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolRatings_Schools_SchoolId1",
                table: "SchoolRatings",
                column: "SchoolId1",
                principalTable: "Schools",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolRatings_AspNetUsers_SchoolAdminId",
                table: "SchoolRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolRatings_Schools_SchoolId1",
                table: "SchoolRatings");

            migrationBuilder.DropIndex(
                name: "IX_SchoolRatings_SchoolAdminId",
                table: "SchoolRatings");

            migrationBuilder.DropIndex(
                name: "IX_SchoolRatings_SchoolId1",
                table: "SchoolRatings");

            migrationBuilder.DropColumn(
                name: "SchoolAdminId",
                table: "SchoolRatings");

            migrationBuilder.DropColumn(
                name: "SchoolId1",
                table: "SchoolRatings");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SchoolRatings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RatingValue",
                table: "SchoolRatings",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
