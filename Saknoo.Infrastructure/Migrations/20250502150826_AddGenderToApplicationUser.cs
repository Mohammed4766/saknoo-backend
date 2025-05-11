using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saknoo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

                migrationBuilder.Sql("UPDATE Users SET Gender = 1"); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");
        }
    }
}
