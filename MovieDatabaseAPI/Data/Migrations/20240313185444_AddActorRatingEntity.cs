using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDatabaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddActorRatingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Actors_ActorId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ActorId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "ActorRatings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorRatings", x => new { x.UserId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_ActorRatings_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorRatings_ActorId",
                table: "ActorRatings",
                column: "ActorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorRatings");

            migrationBuilder.AddColumn<Guid>(
                name: "ActorId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ActorId",
                table: "Comments",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Actors_ActorId",
                table: "Comments",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");
        }
    }
}
