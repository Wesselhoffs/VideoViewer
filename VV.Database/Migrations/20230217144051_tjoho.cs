using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VV.Database.Migrations
{
	/// <inheritdoc />
	public partial class tjoho : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Videos_Directors_DirectorId",
				table: "Videos");

			migrationBuilder.AlterColumn<string>(
				name: "ThumbnailUrl",
				table: "Videos",
				type: "nvarchar(1000)",
				maxLength: 1000,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");

			migrationBuilder.AlterColumn<int>(
				name: "DirectorId",
				table: "Videos",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AddForeignKey(
				name: "FK_Videos_Directors_DirectorId",
				table: "Videos",
				column: "DirectorId",
				principalTable: "Directors",
				principalColumn: "Id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Videos_Directors_DirectorId",
				table: "Videos");

			migrationBuilder.AlterColumn<string>(
				name: "ThumbnailUrl",
				table: "Videos",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(1000)",
				oldMaxLength: 1000);

			migrationBuilder.AlterColumn<int>(
				name: "DirectorId",
				table: "Videos",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AddForeignKey(
				name: "FK_Videos_Directors_DirectorId",
				table: "Videos",
				column: "DirectorId",
				principalTable: "Directors",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
