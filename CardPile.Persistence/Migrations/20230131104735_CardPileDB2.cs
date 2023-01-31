using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardPile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CardPileDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeckListName",
                table: "DeckList",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CardName",
                table: "Card",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DeckList",
                newName: "DeckListName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Card",
                newName: "CardName");
        }
    }
}
