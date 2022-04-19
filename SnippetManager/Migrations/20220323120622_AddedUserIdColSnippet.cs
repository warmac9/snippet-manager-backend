using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnippetManager.Migrations
{
    public partial class AddedUserIdColSnippet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Snippet",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Snippet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Snippet");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Snippet",
                newName: "ID");
        }
    }
}
