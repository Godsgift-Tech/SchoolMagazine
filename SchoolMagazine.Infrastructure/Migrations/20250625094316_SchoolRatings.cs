using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SchoolRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Schools");

            migrationBuilder.CreateTable(
                name: "SchoolRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolRatings_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolRatings_SchoolId",
                table: "SchoolRatings",
                column: "SchoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolRatings");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Schools",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
