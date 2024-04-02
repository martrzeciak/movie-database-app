using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDatabaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateActorImageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActorImages_ActorId",
                table: "ActorImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ActorImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "ActorImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ActorImages_ActorId",
                table: "ActorImages",
                column: "ActorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActorImages_ActorId",
                table: "ActorImages");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ActorImages");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "ActorImages");

            migrationBuilder.CreateIndex(
                name: "IX_ActorImages_ActorId",
                table: "ActorImages",
                column: "ActorId",
                unique: true);
        }
    }
}
