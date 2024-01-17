﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetifyAPI.Data;

#nullable disable

namespace NetifyAPI.Migrations
{
    [DbContext(typeof(NetifyContext))]
    [Migration("20240116012338_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NetifyAPI.Models.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistId"), 1L, 1);

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("SpotifyArtistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("NetifyAPI.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.ArtistGenre", b =>
                {
                    b.Property<int>("ArtistGenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistGenreId"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("ArtistGenreId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("ArtistGenre");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.ArtistTrack", b =>
                {
                    b.Property<int>("ArtistTrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistTrackId"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("ArtistTrackId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("TrackId");

                    b.ToTable("ArtistTrack");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.TrackArtist", b =>
                {
                    b.Property<int>("TrackArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackArtistId"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("TrackArtistId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackArtist");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.TrackGenre", b =>
                {
                    b.Property<int>("TrackGenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackGenreId"), 1L, 1);

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("TrackGenreId");

                    b.HasIndex("GenreId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackGenre");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.UserArtist", b =>
                {
                    b.Property<int>("UserArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserArtistId"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserArtistId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("UserId");

                    b.ToTable("UserArtist");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.UserGenre", b =>
                {
                    b.Property<int>("UserGenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserGenreId"), 1L, 1);

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserGenreId");

                    b.HasIndex("GenreId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGenre");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.UserTrack", b =>
                {
                    b.Property<int>("UserTrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTrackId"), 1L, 1);

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserTrackId");

                    b.HasIndex("TrackId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTrack");
                });

            modelBuilder.Entity("NetifyAPI.Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackId"), 1L, 1);

                    b.Property<int?>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("SpotifySongId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrackId");

                    b.HasIndex("GenreId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("NetifyAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NetifyAPI.Models.Artist", b =>
                {
                    b.HasOne("NetifyAPI.Models.Genre", null)
                        .WithMany("Artists")
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.ArtistGenre", b =>
                {
                    b.HasOne("NetifyAPI.Models.Artist", "Artists")
                        .WithMany("ArtistGenres")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetifyAPI.Models.Genre", "Genres")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artists");

                    b.Navigation("Genres");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.ArtistTrack", b =>
                {
                    b.HasOne("NetifyAPI.Models.Artist", "Artists")
                        .WithMany("ArtistTracks")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetifyAPI.Models.Track", "Tracks")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artists");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.TrackArtist", b =>
                {
                    b.HasOne("NetifyAPI.Models.Artist", "Artists")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetifyAPI.Models.Track", "Tracks")
                        .WithMany("TrackArtists")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artists");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.TrackGenre", b =>
                {
                    b.HasOne("NetifyAPI.Models.Genre", "Genres")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetifyAPI.Models.Track", "Tracks")
                        .WithMany("TrackGenres")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genres");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.UserArtist", b =>
                {
                    b.HasOne("NetifyAPI.Models.Artist", "Artists")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetifyAPI.Models.User", "User")
                        .WithMany("UserArtists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artists");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.UserGenre", b =>
                {
                    b.HasOne("NetifyAPI.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetifyAPI.Models.User", "User")
                        .WithMany("UserGenres")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NetifyAPI.Models.JoinTables.UserTrack", b =>
                {
                    b.HasOne("NetifyAPI.Models.Track", "Tracks")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetifyAPI.Models.User", "User")
                        .WithMany("UserTracks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tracks");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NetifyAPI.Models.Track", b =>
                {
                    b.HasOne("NetifyAPI.Models.Genre", null)
                        .WithMany("Tracks")
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("NetifyAPI.Models.Artist", b =>
                {
                    b.Navigation("ArtistGenres");

                    b.Navigation("ArtistTracks");
                });

            modelBuilder.Entity("NetifyAPI.Models.Genre", b =>
                {
                    b.Navigation("Artists");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("NetifyAPI.Models.Track", b =>
                {
                    b.Navigation("TrackArtists");

                    b.Navigation("TrackGenres");
                });

            modelBuilder.Entity("NetifyAPI.Models.User", b =>
                {
                    b.Navigation("UserArtists");

                    b.Navigation("UserGenres");

                    b.Navigation("UserTracks");
                });
#pragma warning restore 612, 618
        }
    }
}