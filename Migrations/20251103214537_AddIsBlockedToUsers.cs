using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RECIPE.Migrations
{
    /// <inheritdoc />
    public partial class AddIsBlockedToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlock",
                table: "Users",
                newName: "IsBlocked");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "Users",
                newName: "IsBlock");
        }
    }
}
