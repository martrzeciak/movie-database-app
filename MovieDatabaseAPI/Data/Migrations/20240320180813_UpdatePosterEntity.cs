using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDatabaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePosterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posters_MovieId",
                table: "Posters");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Posters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Posters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Posters_MovieId",
                table: "Posters",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posters_MovieId",
                table: "Posters");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Posters");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Posters");

            migrationBuilder.CreateIndex(
                name: "IX_Posters_MovieId",
                table: "Posters",
                column: "MovieId",
                unique: true);
        }
    }
}
