using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIP.API.Migrations
{
    public partial class SeededDefaultUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15b2141c-bfdb-4720-b932-c23b95e46863", "1788d927-8e3d-4c6a-85af-c8ecd5f8f2ea", "Admin", "ADMIN" },
                    { "d0dec921-3991-4506-904e-598d1e049fa7", "4c5ab175-8ea2-484b-83e5-e30493c8a282", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "553677ef-05e0-4baa-9298-484bb047c73d", 0, "89b9bf29-8578-41be-9a49-747c4c7cbaaf", "admin@test-admin.com", false, "", "", false, null, "ADMIN@TEST-ADMIN.COM", "TEST-ADMIN", "AQAAAAEAACcQAAAAEM0VAvS0gMtbiItgXbLUSsIvylEqp5RqaLQo7/NMswaG/BYrd1GvuZl8nBz4jTLJWA==", null, false, "0aa9aef2-a55d-4388-8fb5-505edb4bd503", false, "test-admin" },
                    { "7520b2f8-cd10-42ca-b8ea-cd3419257019", 0, "bce51279-cade-445b-bf04-df85b527c635", "user@test-user.com", false, "", "", false, null, "USER@TEST-USER.COM", "TEST-USER", "AQAAAAEAACcQAAAAENfA9NT0e7izJRX5qr0YLFWxq9ksDCT8tPI9Jde1Aa8oEzNZ8fYzz8S1fffA6uXN+A==", null, false, "eb0f3267-f11f-467b-9064-51a0acde6b8f", false, "test-user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "15b2141c-bfdb-4720-b932-c23b95e46863", "553677ef-05e0-4baa-9298-484bb047c73d" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d0dec921-3991-4506-904e-598d1e049fa7", "7520b2f8-cd10-42ca-b8ea-cd3419257019" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "15b2141c-bfdb-4720-b932-c23b95e46863", "553677ef-05e0-4baa-9298-484bb047c73d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d0dec921-3991-4506-904e-598d1e049fa7", "7520b2f8-cd10-42ca-b8ea-cd3419257019" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15b2141c-bfdb-4720-b932-c23b95e46863");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0dec921-3991-4506-904e-598d1e049fa7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "553677ef-05e0-4baa-9298-484bb047c73d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7520b2f8-cd10-42ca-b8ea-cd3419257019");
        }
    }
}
