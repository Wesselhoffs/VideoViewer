using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VV.Database.Migrations
{
	/// <inheritdoc />
	public partial class test : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Directors",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Directors", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Genres",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Genres", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Videos",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
					Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
					Released = table.Column<DateTime>(type: "datetime2", nullable: false),
					Free = table.Column<bool>(type: "bit", nullable: false),
					DirectorId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Videos", x => x.Id);
					table.ForeignKey(
						name: "FK_Videos_Directors_DirectorId",
						column: x => x.DirectorId,
						principalTable: "Directors",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "SimilarVideos",
				columns: table => new
				{
					VideoId = table.Column<int>(type: "int", nullable: false),
					SimilarVideoId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SimilarVideos", x => new { x.VideoId, x.SimilarVideoId });
					table.ForeignKey(
						name: "FK_SimilarVideos_Videos_SimilarVideoId",
						column: x => x.SimilarVideoId,
						principalTable: "Videos",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_SimilarVideos_Videos_VideoId",
						column: x => x.VideoId,
						principalTable: "Videos",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "VideoGenres",
				columns: table => new
				{
					VideoId = table.Column<int>(type: "int", nullable: false),
					GenreId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_VideoGenres", x => new { x.VideoId, x.GenreId });
					table.ForeignKey(
						name: "FK_VideoGenres_Genres_GenreId",
						column: x => x.GenreId,
						principalTable: "Genres",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_VideoGenres_Videos_VideoId",
						column: x => x.VideoId,
						principalTable: "Videos",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_SimilarVideos_SimilarVideoId",
				table: "SimilarVideos",
				column: "SimilarVideoId");

			migrationBuilder.CreateIndex(
				name: "IX_VideoGenres_GenreId",
				table: "VideoGenres",
				column: "GenreId");

			migrationBuilder.CreateIndex(
				name: "IX_Videos_DirectorId",
				table: "Videos",
				column: "DirectorId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "SimilarVideos");

			migrationBuilder.DropTable(
				name: "VideoGenres");

			migrationBuilder.DropTable(
				name: "Genres");

			migrationBuilder.DropTable(
				name: "Videos");

			migrationBuilder.DropTable(
				name: "Directors");
		}
	}
}
