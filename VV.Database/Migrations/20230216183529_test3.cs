using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VV.Database.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoGenres_VideoGenreVideoId_VideoGenreGenreId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_VideoGenreVideoId_VideoGenreGenreId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoGenreGenreId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoGenreVideoId",
                table: "Videos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoGenreGenreId",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoGenreVideoId",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_VideoGenreVideoId_VideoGenreGenreId",
                table: "Videos",
                columns: new[] { "VideoGenreVideoId", "VideoGenreGenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoGenres_VideoGenreVideoId_VideoGenreGenreId",
                table: "Videos",
                columns: new[] { "VideoGenreVideoId", "VideoGenreGenreId" },
                principalTable: "VideoGenres",
                principalColumns: new[] { "VideoId", "GenreId" });
        }
    }
}
