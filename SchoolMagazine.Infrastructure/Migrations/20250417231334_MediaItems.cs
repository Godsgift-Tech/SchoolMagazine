using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MediaItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "SchoolAdvertMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolAdvertId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolAdvertMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolAdvertMedias_Adverts_SchoolAdvertId",
                        column: x => x.SchoolAdvertId,
                        principalTable: "Adverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolEventMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolEventMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolEventMedias_Events_SchoolEventId",
                        column: x => x.SchoolEventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolAdvertMedias_SchoolAdvertId",
                table: "SchoolAdvertMedias",
                column: "SchoolAdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEventMedias_SchoolEventId",
                table: "SchoolEventMedias",
                column: "SchoolEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolAdvertMedias");

            migrationBuilder.DropTable(
                name: "SchoolEventMedias");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
