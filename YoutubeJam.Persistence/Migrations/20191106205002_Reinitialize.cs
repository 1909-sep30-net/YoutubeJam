using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace YoutubeJam.Persistence.Migrations
{
    public partial class Reinitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Creator",
                columns: table => new
                {
                    CID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creator", x => x.CID);
                });

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

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    VID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    URL = table.Column<string>(nullable: false),
                    VideoChannelChannelID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.VID);
                    table.ForeignKey(
                        name: "FK_Video_Channel_VideoChannelChannelID",
                        column: x => x.VideoChannelChannelID,
                        principalTable: "Channel",
                        principalColumn: "ChannelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Analysis1",
                columns: table => new
                {
                    Anal1ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatrCID = table.Column<int>(nullable: true),
                    VID = table.Column<int>(nullable: true),
                    SentAve = table.Column<decimal>(nullable: false),
                    AnalDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysis1", x => x.Anal1ID);
                    table.ForeignKey(
                        name: "FK_Analysis1_Creator_CreatrCID",
                        column: x => x.CreatrCID,
                        principalTable: "Creator",
                        principalColumn: "CID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Analysis1_Video_VID",
                        column: x => x.VID,
                        principalTable: "Video",
                        principalColumn: "VID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analysis1_CreatrCID",
                table: "Analysis1",
                column: "CreatrCID");

            migrationBuilder.CreateIndex(
                name: "IX_Analysis1_VID",
                table: "Analysis1",
                column: "VID");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_ChannelAuthorCID",
                table: "Channel",
                column: "ChannelAuthorCID");

            migrationBuilder.CreateIndex(
                name: "IX_Video_VideoChannelChannelID",
                table: "Video",
                column: "VideoChannelChannelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analysis1");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Creator");
        }
    }
}
