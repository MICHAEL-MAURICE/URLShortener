using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "URLShorteners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortURl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LongURl = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URLShorteners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_URLShorteners_LongURl",
                table: "URLShorteners",
                column: "LongURl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_URLShorteners_ShortURl",
                table: "URLShorteners",
                column: "ShortURl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "URLShorteners");
        }
    }
}
