using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardPile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AccountIDUserToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountID",
                table: "UserToken",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "UserToken");
        }
    }
}
