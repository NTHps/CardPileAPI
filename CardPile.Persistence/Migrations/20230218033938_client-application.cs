using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardPile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class clientapplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientApplication",
                columns: table => new
                {
                    ClientApplicationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AccessToken = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientApplication", x => x.ClientApplicationID);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_ClientApplication_Name",
                table: "ClientApplication",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientApplication");
        }
    }
}
