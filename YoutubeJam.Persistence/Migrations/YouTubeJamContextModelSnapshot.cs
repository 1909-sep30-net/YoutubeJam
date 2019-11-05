﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using YoutubeJam.Persistence.Entities;

namespace YoutubeJam.Persistence.Migrations
{
    [DbContext(typeof(YouTubeJamContext))]
    partial class YouTubeJamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("YoutubeJam.Persistence.Entities.Analysis1", b =>
                {
                    b.Property<int>("Anal1ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("AnalDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CreatrCID")
                        .HasColumnType("integer");

                    b.Property<decimal>("SentAve")
                        .HasColumnType("numeric");

                    b.Property<int?>("VID")
                        .HasColumnType("integer");

                    b.HasKey("Anal1ID");

                    b.HasIndex("CreatrCID");

                    b.HasIndex("VID");

                    b.ToTable("Analysis1");
                });

            modelBuilder.Entity("YoutubeJam.Persistence.Entities.Channel", b =>
                {
                    b.Property<int>("ChannelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ChannelAuthorCID")
                        .HasColumnType("integer");

                    b.Property<string>("ChannelName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ChannelID");

                    b.HasIndex("ChannelAuthorCID");

                    b.ToTable("Channel");
                });

            modelBuilder.Entity("YoutubeJam.Persistence.Entities.Creator", b =>
                {
                    b.Property<int>("CID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CID");

                    b.ToTable("Creator");
                });

            modelBuilder.Entity("YoutubeJam.Persistence.Entities.Video", b =>
                {
                    b.Property<int>("VID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("VideoChannelChannelID")
                        .HasColumnType("integer");

                    b.HasKey("VID");

                    b.HasIndex("VideoChannelChannelID");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("YoutubeJam.Persistence.Entities.Analysis1", b =>
                {
                    b.HasOne("YoutubeJam.Persistence.Entities.Creator", "Creatr")
                        .WithMany()
                        .HasForeignKey("CreatrCID");

                    b.HasOne("YoutubeJam.Persistence.Entities.Video", "Vid")
                        .WithMany()
                        .HasForeignKey("VID");
                });

            modelBuilder.Entity("YoutubeJam.Persistence.Entities.Channel", b =>
                {
                    b.HasOne("YoutubeJam.Persistence.Entities.Creator", "ChannelAuthor")
                        .WithMany()
                        .HasForeignKey("ChannelAuthorCID");
                });

            modelBuilder.Entity("YoutubeJam.Persistence.Entities.Video", b =>
                {
                    b.HasOne("YoutubeJam.Persistence.Entities.Channel", "VideoChannel")
                        .WithMany()
                        .HasForeignKey("VideoChannelChannelID");
                });
#pragma warning restore 612, 618
        }
    }
}
