using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangScan.Migrations
{
    /// <inheritdoc />
    public partial class UnlockLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LearningAllowed",
                value: true);

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 3L,
                column: "KnownAllowed",
                value: true);

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 4L,
                column: "KnownAllowed",
                value: true);

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 5L,
                column: "KnownAllowed",
                value: true);

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 6L,
                column: "KnownAllowed",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LearningAllowed",
                value: false);

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 3L,
                column: "KnownAllowed",
                value: false);

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 4L,
                column: "KnownAllowed",
                value: false);

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 5L,
                column: "KnownAllowed",
                value: false);

            migrationBuilder.UpdateData(
                table: "languages",
                keyColumn: "Id",
                keyValue: 6L,
                column: "KnownAllowed",
                value: false);
        }
    }
}
