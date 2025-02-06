using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52ecd840-7b0b-4b1e-887e-95d8e63cc1cc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f4a9b4d4-6138-45e0-ba58-b1121ce64825"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("632100bf-80bf-4603-9997-d6a013964c4a"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("52ecd840-7b0b-4b1e-887e-95d8e63cc1cc"), "445bbdb7-9f42-4150-92d0-e6470089549e", "Admin", "ADMIN" },
                    { new Guid("f4a9b4d4-6138-45e0-ba58-b1121ce64825"), "f45722cc-6da6-4aae-ad1e-533e041e915a", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("632100bf-80bf-4603-9997-d6a013964c4a"), 0, "c7b9d162-449c-4b3e-a04b-550978fc2a2f", "godsgiftoghenechohwo@gmail.com", true, "Admin", "Luxe", false, null, "GODSGIFTOGHENECHOHWO@GMAIL.COM", "GODSGIFTOGHENECHOHWO@GMAIL.COM", "AQAAAAIAAYagAAAAEDT4OtHIH0QS8N36nuqTodeSm1FtJagi24/a7e6swexklwMID2GgJWZfYkX0q0ejXg==", null, false, "542a74bc-f054-4846-97a7-61c2f0e9f22c", false, "godsgiftoghenechohwo@gmail.com" });
        }
    }
}
