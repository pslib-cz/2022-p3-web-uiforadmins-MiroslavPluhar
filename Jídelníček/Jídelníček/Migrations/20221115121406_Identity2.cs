using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jídelníček.Migrations
{
    public partial class Identity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "1aebc147-7657-420e-bf46-8ea507c2bfea", "miroslav.pluhar.020@pslib.cz", true, "MIROSLAV.PLUHAR.020@PSLIB.CZ", "MIROSLAV.PLUHAR.020@PSLIB.CZ", "AQAAAAEAACcQAAAAEHytuZhEqfOwtWV8QMbNdHNtLbuVNEiFyefGCuIt3MJnfzgOr+YrQjUfSKX7kttLnA==", "miroslav.pluhar.020@pslib.cz" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "877b66eb-6a74-4473-97a3-a412ec5c7370", null, false, null, null, null, null });
        }
    }
}
