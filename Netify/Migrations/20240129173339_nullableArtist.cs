using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetifyAPI.Migrations
{
    public partial class nullableArtist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTrack_Artists_ArtistId",
                table: "ArtistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTrack_Tracks_TrackId",
                table: "ArtistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Genres_GenreId",
                table: "Tracks");

            migrationBuilder.DropTable(
                name: "ArtistGenre");

            migrationBuilder.DropTable(
                name: "TrackArtist");

            migrationBuilder.DropTable(
                name: "TrackGenre");

            migrationBuilder.DropTable(
                name: "UserArtist");

            migrationBuilder.DropTable(
                name: "UserGenre");

            migrationBuilder.DropTable(
                name: "UserTrack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistTrack",
                table: "ArtistTrack");

            migrationBuilder.DropIndex(
                name: "IX_ArtistTrack_ArtistId",
                table: "ArtistTrack");

            migrationBuilder.DropColumn(
                name: "ArtistTrackId",
                table: "ArtistTrack");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Tracks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tracks_GenreId",
                table: "Tracks",
                newName: "IX_Tracks_UserId");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "ArtistTrack",
                newName: "TracksTrackId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "ArtistTrack",
                newName: "ArtistsArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistTrack_TrackId",
                table: "ArtistTrack",
                newName: "IX_ArtistTrack_TracksTrackId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Artists",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistTrack",
                table: "ArtistTrack",
                columns: new[] { "ArtistsArtistId", "TracksTrackId" });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_UserId",
                table: "Genres",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UserId",
                table: "Artists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_UserId",
                table: "Artists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTrack_Artists_ArtistsArtistId",
                table: "ArtistTrack",
                column: "ArtistsArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTrack_Tracks_TracksTrackId",
                table: "ArtistTrack",
                column: "TracksTrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_UserId",
                table: "Genres",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Users_UserId",
                table: "Tracks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_UserId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTrack_Artists_ArtistsArtistId",
                table: "ArtistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTrack_Tracks_TracksTrackId",
                table: "ArtistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_UserId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Users_UserId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Genres_UserId",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistTrack",
                table: "ArtistTrack");

            migrationBuilder.DropIndex(
                name: "IX_Artists_UserId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Artists");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tracks",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Tracks_UserId",
                table: "Tracks",
                newName: "IX_Tracks_GenreId");

            migrationBuilder.RenameColumn(
                name: "TracksTrackId",
                table: "ArtistTrack",
                newName: "TrackId");

            migrationBuilder.RenameColumn(
                name: "ArtistsArtistId",
                table: "ArtistTrack",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistTrack_TracksTrackId",
                table: "ArtistTrack",
                newName: "IX_ArtistTrack_TrackId");

            migrationBuilder.AddColumn<int>(
                name: "ArtistTrackId",
                table: "ArtistTrack",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistTrack",
                table: "ArtistTrack",
                column: "ArtistTrackId");

            migrationBuilder.CreateTable(
                name: "ArtistGenre",
                columns: table => new
                {
                    ArtistGenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistGenre", x => x.ArtistGenreId);
                    table.ForeignKey(
                        name: "FK_ArtistGenre_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackArtist",
                columns: table => new
                {
                    TrackArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackArtist", x => x.TrackArtistId);
                    table.ForeignKey(
                        name: "FK_TrackArtist_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackArtist_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackGenre",
                columns: table => new
                {
                    TrackGenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackGenre", x => x.TrackGenreId);
                    table.ForeignKey(
                        name: "FK_TrackGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackGenre_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserArtist",
                columns: table => new
                {
                    UserArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArtist", x => x.UserArtistId);
                    table.ForeignKey(
                        name: "FK_UserArtist_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArtist_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGenre",
                columns: table => new
                {
                    UserGenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGenre", x => x.UserGenreId);
                    table.ForeignKey(
                        name: "FK_UserGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGenre_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrack",
                columns: table => new
                {
                    UserTrackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrack", x => x.UserTrackId);
                    table.ForeignKey(
                        name: "FK_UserTrack_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrack_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistTrack_ArtistId",
                table: "ArtistTrack",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistGenre_ArtistId",
                table: "ArtistGenre",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistGenre_GenreId",
                table: "ArtistGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackArtist_ArtistId",
                table: "TrackArtist",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackArtist_TrackId",
                table: "TrackArtist",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackGenre_GenreId",
                table: "TrackGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackGenre_TrackId",
                table: "TrackGenre",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArtist_ArtistId",
                table: "UserArtist",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArtist_UserId",
                table: "UserArtist",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGenre_GenreId",
                table: "UserGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGenre_UserId",
                table: "UserGenre",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrack_TrackId",
                table: "UserTrack",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrack_UserId",
                table: "UserTrack",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTrack_Artists_ArtistId",
                table: "ArtistTrack",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTrack_Tracks_TrackId",
                table: "ArtistTrack",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Genres_GenreId",
                table: "Tracks",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId");
        }
    }
}
