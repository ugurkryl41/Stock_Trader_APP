using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    PortfolioId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioItems_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Portfolios",
                columns: new[] { "Id", "UserId", "userBalance" },
                values: new object[,]
                {
                    { 1, "user1", 1000.00m },
                    { 2, "user2", 2000.00m },
                    { 3, "user3", 3000.00m },
                    { 4, "user4", 4000.00m },
                    { 5, "user5", 5000.00m }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Price", "Symbol" },
                values: new object[,]
                {
                    { 1, 100.00m, "ABC" },
                    { 2, 200.00m, "DEF" },
                    { 3, 300.00m, "GHI" },
                    { 4, 400.00m, "JKL" },
                    { 5, 500.00m, "MNO" }
                });

            migrationBuilder.InsertData(
                table: "PortfolioItems",
                columns: new[] { "Id", "PortfolioId", "Quantity", "StockId" },
                values: new object[,]
                {
                    { 1, 1, 10, 1 },
                    { 2, 2, 20, 2 },
                    { 3, 3, 30, 3 },
                    { 4, 4, 40, 4 },
                    { 5, 5, 50, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItems_PortfolioId",
                table: "PortfolioItems",
                column: "PortfolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioItems");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Portfolios");
        }
    }
}
