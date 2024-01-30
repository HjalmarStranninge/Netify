using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetifyAPI.Migrations
{
    public partial class NullableGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Genre_MainGenreGenreId",
                table: "Artists");

            migrationBuilder.AlterColumn<int>(
                name: "MainGenreGenreId",
                table: "Artists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Genre_MainGenreGenreId",
                table: "Artists",
                column: "MainGenreGenreId",
                principalTable: "Genre",
                principalColumn: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Genre_MainGenreGenreId",
                table: "Artists");

            migrationBuilder.AlterColumn<int>(
                name: "MainGenreGenreId",
                table: "Artists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Genre_MainGenreGenreId",
                table: "Artists",
                column: "MainGenreGenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
