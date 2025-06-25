using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedSchoolRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "SchoolRatings");

            migrationBuilder.AddColumn<int>(
                name: "RatingValue",
                table: "SchoolRatings",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolRatings_UserId",
                table: "SchoolRatings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolRatings_AspNetUsers_UserId",
                table: "SchoolRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolRatings_AspNetUsers_UserId",
                table: "SchoolRatings");

            migrationBuilder.DropIndex(
                name: "IX_SchoolRatings_UserId",
                table: "SchoolRatings");

            migrationBuilder.DropColumn(
                name: "RatingValue",
                table: "SchoolRatings");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "SchoolRatings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
