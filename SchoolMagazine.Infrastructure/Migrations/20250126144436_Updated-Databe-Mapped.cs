using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDatabeMapped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Schools",
                newName: "EmailAddress");

            migrationBuilder.AddColumn<string>(
                name: "AdminName",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminName",
                table: "Schools");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Schools",
                newName: "Name");
        }
    }
}
