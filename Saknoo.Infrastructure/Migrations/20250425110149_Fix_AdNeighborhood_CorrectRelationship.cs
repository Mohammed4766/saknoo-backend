using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saknoo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_AdNeighborhood_CorrectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdNeighborhood_Neighborhoods_NeighborhoodId1",
                table: "AdNeighborhood");

            migrationBuilder.DropIndex(
                name: "IX_AdNeighborhood_NeighborhoodId1",
                table: "AdNeighborhood");

            migrationBuilder.DropColumn(
                name: "NeighborhoodId1",
                table: "AdNeighborhood");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodId1",
                table: "AdNeighborhood",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdNeighborhood_NeighborhoodId1",
                table: "AdNeighborhood",
                column: "NeighborhoodId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AdNeighborhood_Neighborhoods_NeighborhoodId1",
                table: "AdNeighborhood",
                column: "NeighborhoodId1",
                principalTable: "Neighborhoods",
                principalColumn: "Id");
        }
    }
}
