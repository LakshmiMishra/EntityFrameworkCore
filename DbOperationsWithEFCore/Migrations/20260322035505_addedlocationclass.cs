using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbOperationsWithEFCore.Migrations
{
    /// <inheritdoc />
    public partial class addedlocationclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageId",
                table: "Books",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Language_LanguageId",
                table: "Books",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Language_LanguageId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Books_LanguageId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Books");
        }
    }
}
