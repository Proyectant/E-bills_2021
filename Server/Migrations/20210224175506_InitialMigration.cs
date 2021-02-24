using Microsoft.EntityFrameworkCore.Migrations;

namespace ERacuniNovi.Server.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df565f74-7d72-4460-a54b-c48fc3b65157");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "firstName", "lastName" },
                values: new object[] { "df565f74-7d72-4460-a54b-c48fc3b65157", 0, "e0f7c8a0-afc5-4c90-8b0a-9ee5e5a05767", "User", null, false, false, null, null, null, null, null, false, "a49a3b0d-c24b-4fee-9fab-478983cffc8d", false, "proyectant", "Almedin", "Ljajic" });
        }
    }
}
