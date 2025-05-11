using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saknoo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_ApplicationUserId_From_MatchingAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchingAnswers_Users_ApplicationUserId",
                table: "MatchingAnswers");

            migrationBuilder.DropIndex(
                name: "IX_MatchingAnswers_ApplicationUserId",
                table: "MatchingAnswers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "MatchingAnswers");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "MatchingAnswers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchingAnswers_ApplicationUserId",
                table: "MatchingAnswers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingAnswers_Users_ApplicationUserId",
                table: "MatchingAnswers",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");

        }
    }
}
