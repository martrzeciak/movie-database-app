using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDatabaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserImageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages");

            migrationBuilder.DropColumn(
                name: "KnownAs",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "UserImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "UserImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "UserImages");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "UserImages");

            migrationBuilder.AddColumn<string>(
                name: "KnownAs",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId",
                unique: true);
        }
    }
}
