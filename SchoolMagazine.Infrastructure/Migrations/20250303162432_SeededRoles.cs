using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMagazine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeededRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Define static GUIDs for roles
            var superAdminRoleId = "11111111-1111-1111-1111-111111111111";
            var schoolAdminRoleId = "22222222-2222-2222-2222-222222222222";
            var userRoleId = "33333333-3333-3333-3333-333333333333";

            // Insert Roles
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
            { superAdminRoleId, "SuperAdmin", "SUPERADMIN", Guid.NewGuid().ToString() },
            { schoolAdminRoleId, "SchoolAdmin", "SCHOOLADMIN", Guid.NewGuid().ToString() },
            { userRoleId, "User", "USER", Guid.NewGuid().ToString() }
                });

            // Define static GUIDs for users
            var superAdminUserId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa";
            var schoolAdminUserId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb";
            var normalUserId = "cccccccc-cccc-cccc-cccc-cccccccccccc";

            // Pre-hashed passwords (use a tool to generate them)
            var superAdminPasswordHash = "AQAAAAEAACcQAAAAEMsHr+8UO+MQqNH8C0ePyT5Yr5hGq1s5d+BRlFsyOD+6JmRn==";
            var schoolAdminPasswordHash = "AQAAAAEAACcQAAAAEKFcvwvczDsOZ/F2e+wFYzhIm4TYolQewqUWhD8R5+Uv==";
            var userPasswordHash = "AQAAAAEAACcQAAAAEJtnGZ+ciEjCcVq/EY8D6JpgZJeSokS23==";

            // Insert Users with all required fields
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
            "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
            "EmailConfirmed", "PasswordHash", "SecurityStamp",
            "FirstName", "LastName",
            "AccessFailedCount", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled"
                },
                values: new object[,]
                {
            { superAdminUserId, "superadmin", "SUPERADMIN", "superadmin@example.com", "SUPERADMIN@EXAMPLE.COM", true, superAdminPasswordHash, "STATIC-STAMP-SUPERADMIN", "OgaAdmin", "Oga", 0, false, false, false },
            { schoolAdminUserId, "schooladmin", "SCHOOLADMIN", "schooladmin@example.com", "SCHOOLADMIN@EXAMPLE.COM", true, schoolAdminPasswordHash, "STATIC-STAMP-SCHOOLADMIN", "Principal", "OgaP", 0, false, false, false },
            { normalUserId, "regularuser", "REGULARUSER", "user@example.com", "USER@EXAMPLE.COM", true, userPasswordHash, "STATIC-STAMP-USER", "User", "OgaUser", 0, false, false, false }
                });

            // Assign Roles to Users
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
            { superAdminUserId, superAdminRoleId },
            { schoolAdminUserId, schoolAdminRoleId },
            { normalUserId, userRoleId }
                });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "AspNetUserRoles", keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", "11111111-1111-1111-1111-111111111111" });

            migrationBuilder.DeleteData(table: "AspNetUserRoles", keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", "22222222-2222-2222-2222-222222222222" });

            migrationBuilder.DeleteData(table: "AspNetUserRoles", keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "cccccccc-cccc-cccc-cccc-cccccccccccc", "33333333-3333-3333-3333-333333333333" });

            migrationBuilder.DeleteData(table: "AspNetUsers", keyColumn: "Id", keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            migrationBuilder.DeleteData(table: "AspNetUsers", keyColumn: "Id", keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            migrationBuilder.DeleteData(table: "AspNetUsers", keyColumn: "Id", keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc");

            migrationBuilder.DeleteData(table: "AspNetRoles", keyColumn: "Id", keyValue: "11111111-1111-1111-1111-111111111111");
            migrationBuilder.DeleteData(table: "AspNetRoles", keyColumn: "Id", keyValue: "22222222-2222-2222-2222-222222222222");
            migrationBuilder.DeleteData(table: "AspNetRoles", keyColumn: "Id", keyValue: "33333333-3333-3333-3333-333333333333");
        }
    }
}
