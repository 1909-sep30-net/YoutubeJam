using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeJam.Persistence.Migrations
{
    public partial class AddedUsernametoCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Creator",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Creator");
        }
    }
}
