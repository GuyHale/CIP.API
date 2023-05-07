using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIP.API.Migrations
{
    public partial class AddedApiKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15b2141c-bfdb-4720-b932-c23b95e46863",
                column: "ConcurrencyStamp",
                value: "fdabc1ed-015b-4a42-9601-c7ae922ab5dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0dec921-3991-4506-904e-598d1e049fa7",
                column: "ConcurrencyStamp",
                value: "f17f4829-0719-47e7-baa0-c5504d0c5b1f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "553677ef-05e0-4baa-9298-484bb047c73d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "632b5d94-4b23-45df-83b2-f4baecac3012", "AQAAAAEAACcQAAAAEBO3qamRuf8mRkvcXoaM6DpXbn0jxDmRH/RUwSyfWPagLIUoUVNYjW7F4CGQSmH+pQ==", "627f0290-23eb-4eb6-b4e4-581e346a5f0a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7520b2f8-cd10-42ca-b8ea-cd3419257019",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b2bdb00-a1bd-4054-8dd2-0e83107b7e46", "AQAAAAEAACcQAAAAEKW1vDCI3VXqSDoB7v+YE3NOqNANknrkgYwhd0Ov2KZ6PPlk750x/Sf1g7bF85Wm6w==", "2c8ab17c-a797-47f0-9fda-09e640cf9f27" });

            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15b2141c-bfdb-4720-b932-c23b95e46863",
                column: "ConcurrencyStamp",
                value: "1788d927-8e3d-4c6a-85af-c8ecd5f8f2ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0dec921-3991-4506-904e-598d1e049fa7",
                column: "ConcurrencyStamp",
                value: "4c5ab175-8ea2-484b-83e5-e30493c8a282");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "553677ef-05e0-4baa-9298-484bb047c73d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89b9bf29-8578-41be-9a49-747c4c7cbaaf", "AQAAAAEAACcQAAAAEM0VAvS0gMtbiItgXbLUSsIvylEqp5RqaLQo7/NMswaG/BYrd1GvuZl8nBz4jTLJWA==", "0aa9aef2-a55d-4388-8fb5-505edb4bd503" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7520b2f8-cd10-42ca-b8ea-cd3419257019",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bce51279-cade-445b-bf04-df85b527c635", "AQAAAAEAACcQAAAAENfA9NT0e7izJRX5qr0YLFWxq9ksDCT8tPI9Jde1Aa8oEzNZ8fYzz8S1fffA6uXN+A==", "eb0f3267-f11f-467b-9064-51a0acde6b8f" });
        }
    }
}
