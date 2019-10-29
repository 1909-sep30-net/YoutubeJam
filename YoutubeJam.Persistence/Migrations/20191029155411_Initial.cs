using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace YoutubeJam.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Creator",
                columns: table => new
                {
                    CID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creator", x => x.CID);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    VID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    URL = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.VID);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analysis1");

            migrationBuilder.DropTable(
                name: "Creator");

            migrationBuilder.DropTable(
                name: "Video");
        }
    }
}
