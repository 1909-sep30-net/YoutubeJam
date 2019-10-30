using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeJam.Persistence.Migrations
{
    public partial class ChangedDatatypeofPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Creator",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                table: "Creator",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
