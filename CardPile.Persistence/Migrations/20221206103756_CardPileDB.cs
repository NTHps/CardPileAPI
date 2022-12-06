using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardPile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CardPileDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeckList",
                columns: table => new
                {
                    DeckListID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckListName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckList", x => x.DeckListID);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeckListID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardID);
                    table.ForeignKey(
                        name: "FK_Card_DeckList_DeckListID",
                        column: x => x.DeckListID,
                        principalTable: "DeckList",
                        principalColumn: "DeckListID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_DeckListID",
                table: "Card",
                column: "DeckListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "DeckList");
        }
    }
}
