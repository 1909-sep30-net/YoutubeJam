using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace YoutubeJam.Persistence.Migrations
{
    public partial class ChannelAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoChannelChannelID",
                table: "Video",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    ChannelID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChannelName = table.Column<string>(nullable: false),
                    ChannelAuthorCID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.ChannelID);
                    table.ForeignKey(
                        name: "FK_Channel_Creator_ChannelAuthorCID",
                        column: x => x.ChannelAuthorCID,
                        principalTable: "Creator",
                        principalColumn: "CID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Video_VideoChannelChannelID",
                table: "Video",
                column: "VideoChannelChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_ChannelAuthorCID",
                table: "Channel",
                column: "ChannelAuthorCID");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Channel_VideoChannelChannelID",
                table: "Video",
                column: "VideoChannelChannelID",
                principalTable: "Channel",
                principalColumn: "ChannelID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_Channel_VideoChannelChannelID",
                table: "Video");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropIndex(
                name: "IX_Video_VideoChannelChannelID",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "VideoChannelChannelID",
                table: "Video");
        }
    }
}
