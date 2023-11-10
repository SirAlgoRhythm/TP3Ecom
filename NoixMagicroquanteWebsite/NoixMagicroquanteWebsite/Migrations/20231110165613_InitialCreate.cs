using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NoixMagicroquanteWebsite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    SellingPrice = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edible = table.Column<bool>(type: "bit", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    BasketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false),
                    SellDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.BasketId);
                    table.ForeignKey(
                        name: "FK_Basket_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    BPProductId = table.Column<int>(type: "int", nullable: false),
                    BPBasketId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => new { x.BPProductId, x.BPBasketId });
                    table.ForeignKey(
                        name: "FK_ProductOrder_Basket_BPProductId",
                        column: x => x.BPProductId,
                        principalTable: "Basket",
                        principalColumn: "BasketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Détergent" },
                    { 2, "Outil nettoyant" },
                    { 3, "Papier" },
                    { 4, "Biscuits" },
                    { 5, "Breuvage" },
                    { 6, "Chocolat" },
                    { 7, "Fruit" },
                    { 8, "Légume" },
                    { 9, "Café" },
                    { 10, "Outil construction" }
                });

            migrationBuilder.InsertData(
                table: "Tax",
                columns: new[] { "Id", "Name", "Rate" },
                values: new object[,]
                {
                    { 1, "TPS + TVQ 5% + 9.975%", 14.975f },
                    { 2, "No Taxes", 0f }
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "UnitId", "Name" },
                values: new object[,]
                {
                    { 1, "Item" },
                    { 2, "Bouteille" },
                    { 3, "Barre" },
                    { 4, "Unité" },
                    { 5, "Unité (par kg)" },
                    { 6, "Boite" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "IsAdmin", "LastName", "Password", "UserName" },
                values: new object[] { 1, "admin@noixmagiques.com", "admin", true, "admin", "AQAAAAEAACcQAAAAECp0ROY8Ai0bxYY7vrNEc2AMzZ9riapPYF4eisyY2+wsXUFLUMYsjtDTO3xCV4lrlA==", "admin" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "Edible", "Image", "Name", "PurchasePrice", "SellingPrice", "Stock", "TaxId", "UnitId" },
                values: new object[,]
                {
                    { 1, 1, false, "", "ComLet", 4.0, 8.0, 8, 1, 2 },
                    { 2, 1, false, "", "JaBlex", 7.0, 14.0, 10, 1, 2 },
                    { 3, 1, false, "", "Mr.Blet", 2.0, 4.0, 3, 1, 2 },
                    { 4, 1, false, "", "Pasmalivre", 3.0, 6.0, 0, 1, 2 },
                    { 5, 1, false, "", "Stablex", 5.0, 10.0, 21, 1, 2 },
                    { 6, 2, false, "", "Brosse", 3.0, 6.0, 41, 1, 1 },
                    { 7, 2, false, "", "Balai", 10.0, 20.0, 21, 1, 1 },
                    { 8, 2, false, "", "Serviette", 6.0, 12.0, 30, 1, 1 },
                    { 9, 2, false, "", "Cuve", 12.0, 24.0, 8, 1, 1 },
                    { 10, 2, false, "", "Mopette", 17.0, 34.0, 12, 1, 1 },
                    { 11, 3, false, "", "Mouchoirs", 5.0, 10.0, 60, 1, 1 },
                    { 12, 3, false, "", "Essuie-tout", 6.0, 12.0, 20, 1, 1 },
                    { 13, 3, false, "", "Papier toilette", 9.0, 18.0, 128, 1, 1 },
                    { 14, 4, true, "", "Ore-crisp", 6.0, 12.0, 5, 2, 6 },
                    { 15, 4, true, "", "Crispie-Soda", 3.0, 6.0, 12, 2, 6 },
                    { 16, 4, true, "", "Petit-beurrier", 5.0, 10.0, 20, 2, 6 },
                    { 17, 5, true, "", "Gotorade", 2.0, 4.0, 30, 2, 2 },
                    { 18, 5, true, "", "Lait", 5.0, 10.0, 5, 2, 2 },
                    { 19, 5, true, "", "Oranginol", 1.0, 2.0, 15, 2, 2 },
                    { 20, 6, true, "", "Wondermilk", 1.0, 2.0, 24, 2, 3 },
                    { 21, 6, true, "", "Aeriol", 1.0, 2.0, 32, 2, 3 },
                    { 22, 7, true, "", "Orange", 1.0, 2.0, 30, 2, 4 },
                    { 23, 7, true, "", "Pomme", 1.0, 2.0, 20, 2, 4 },
                    { 24, 7, true, "", "Banane", 2.0, 4.0, 18, 2, 4 },
                    { 25, 7, true, "", "Cantaloup", 5.0, 10.0, 4, 2, 4 },
                    { 26, 8, true, "", "Tomate", 2.0, 4.0, 9, 2, 4 },
                    { 27, 8, true, "", "Cocombre", 2.0, 4.0, 12, 2, 4 },
                    { 28, 9, true, "", "Café corsé", 10.0, 20.0, 20, 2, 5 },
                    { 29, 9, true, "", "Café velouté", 10.0, 20.0, 80, 2, 5 },
                    { 30, 9, true, "", "Café décaféiné", 11.0, 22.0, 30, 2, 5 },
                    { 31, 10, false, "", "Tournevis", 3.0, 6.0, 17, 1, 1 },
                    { 32, 10, false, "", "Scie ronde", 90.0, 180.0, 9, 1, 1 },
                    { 33, 10, false, "", "Marteau", 15.0, 30.0, 27, 1, 1 },
                    { 34, 10, false, "", "Équerre", 10.0, 20.0, 18, 1, 1 },
                    { 35, 10, false, "", "Ruban à mesurer", 12.0, 24.0, 13, 1, 1 },
                    { 36, 10, false, "", "Clous 1 pouce", 4.0, 8.0, 8, 1, 6 },
                    { 37, 10, false, "", "Clous 2 pouces", 6.0, 12.0, 6, 1, 6 },
                    { 38, 10, false, "", "Clous 3 pouces", 8.0, 16.0, 12, 1, 6 },
                    { 39, 10, false, "", "Clous à béton", 10.0, 20.0, 7, 1, 6 },
                    { 40, 10, false, "", "Clous à finition", 6.0, 12.0, 14, 1, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TaxId",
                table: "Product",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitId",
                table: "Product",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}
