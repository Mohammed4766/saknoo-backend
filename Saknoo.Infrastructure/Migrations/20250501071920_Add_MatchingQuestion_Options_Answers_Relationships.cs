using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saknoo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_MatchingQuestion_Options_Answers_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MatchingQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchingQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchingAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatchingQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchingAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchingAnswers_MatchingQuestions_MatchingQuestionId",
                        column: x => x.MatchingQuestionId,
                        principalTable: "MatchingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchingAnswers_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchingAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchingOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MatchingQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchingOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchingOptions_MatchingQuestions_MatchingQuestionId",
                        column: x => x.MatchingQuestionId,
                        principalTable: "MatchingQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchingAnswers_ApplicationUserId",
                table: "MatchingAnswers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingAnswers_MatchingQuestionId",
                table: "MatchingAnswers",
                column: "MatchingQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingAnswers_UserId",
                table: "MatchingAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingOptions_MatchingQuestionId",
                table: "MatchingOptions",
                column: "MatchingQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchingAnswers");

            migrationBuilder.DropTable(
                name: "MatchingOptions");

            migrationBuilder.DropTable(
                name: "MatchingQuestions");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");
        }
    }
}
