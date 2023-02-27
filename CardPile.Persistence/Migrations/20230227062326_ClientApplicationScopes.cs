using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardPile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ClientApplicationScopes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "UserToken",
                newName: "AccessToken");

            migrationBuilder.AddColumn<int>(
                name: "ExpiresIn",
                table: "UserToken",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserToken",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TokenType",
                table: "UserToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Scope",
                columns: table => new
                {
                    ScopeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scope", x => x.ScopeID);
                });

            migrationBuilder.CreateTable(
                name: "ClientApplicationScope",
                columns: table => new
                {
                    ClientApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    ScopeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientApplicationScope", x => new { x.ClientApplicationID, x.ScopeID });
                    table.ForeignKey(
                        name: "FK_ClientApplicationScope_ClientApplication",
                        column: x => x.ClientApplicationID,
                        principalTable: "ClientApplication",
                        principalColumn: "ClientApplicationID");
                    table.ForeignKey(
                        name: "FK_ClientApplicationScope_Scope",
                        column: x => x.ScopeID,
                        principalTable: "Scope",
                        principalColumn: "ScopeID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientApplicationScope_ScopeID",
                table: "ClientApplicationScope",
                column: "ScopeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientApplicationScope");

            migrationBuilder.DropTable(
                name: "Scope");

            migrationBuilder.DropColumn(
                name: "ExpiresIn",
                table: "UserToken");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserToken");

            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "UserToken");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "UserToken",
                newName: "Token");
        }
    }
}
