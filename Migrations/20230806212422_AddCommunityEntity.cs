using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThreadsBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommunityId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communities_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommunityId",
                table: "Users",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CommunityId",
                table: "Threads",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_CreatedById",
                table: "Communities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_Id",
                table: "Communities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Communities_Username",
                table: "Communities",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Communities_CommunityId",
                table: "Threads",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Communities_CommunityId",
                table: "Users",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Communities_CommunityId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Communities_CommunityId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Users_CommunityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Threads_CommunityId",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Users");
        }
    }
}
