using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningQ.DAL.Migrations
{
    public partial class AddedNotesColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Item");
        }
    }
}
