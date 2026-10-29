using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LangScan.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AnimalHealth = table.Column<int>(type: "integer", nullable: false),
                    AnimalFood = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phrase_groups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IdLanguageKnown = table.Column<long>(type: "bigint", nullable: false),
                    IdLanguageLearning = table.Column<long>(type: "bigint", nullable: false),
                    ProficiencyLevel = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phrase_groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phrase_groups_languages_IdLanguageKnown",
                        column: x => x.IdLanguageKnown,
                        principalTable: "languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_phrase_groups_languages_IdLanguageLearning",
                        column: x => x.IdLanguageLearning,
                        principalTable: "languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_phrase_groups_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phrases",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPhraseGroup = table.Column<long>(type: "bigint", nullable: false),
                    WordKnown = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    WordLearning = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    DefinitionKnown = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    DefinitionLearning = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phrases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phrases_phrase_groups_IdPhraseGroup",
                        column: x => x.IdPhraseGroup,
                        principalTable: "phrase_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "languages",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { 1L, "pl" },
                    { 2L, "en" },
                    { 3L, "de" },
                    { 4L, "es" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_languages_Code",
                table: "languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_phrase_groups_IdLanguageKnown",
                table: "phrase_groups",
                column: "IdLanguageKnown");

            migrationBuilder.CreateIndex(
                name: "IX_phrase_groups_IdLanguageLearning",
                table: "phrase_groups",
                column: "IdLanguageLearning");

            migrationBuilder.CreateIndex(
                name: "IX_phrase_groups_IdUser",
                table: "phrase_groups",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_phrases_IdPhraseGroup",
                table: "phrases",
                column: "IdPhraseGroup");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "phrases");

            migrationBuilder.DropTable(
                name: "phrase_groups");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
