using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VV.Database.Migrations
{
	/// <inheritdoc />
	public partial class ReworkedVideoEntity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_SimilarVideos_Videos_VideoId",
				table: "SimilarVideos");

			migrationBuilder.AddForeignKey(
				name: "FK_SimilarVideos_Videos_VideoId",
				table: "SimilarVideos",
				column: "VideoId",
				principalTable: "Videos",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_SimilarVideos_Videos_VideoId",
				table: "SimilarVideos");

			migrationBuilder.AddForeignKey(
				name: "FK_SimilarVideos_Videos_VideoId",
				table: "SimilarVideos",
				column: "VideoId",
				principalTable: "Videos",
				principalColumn: "Id");
		}
	}
}
