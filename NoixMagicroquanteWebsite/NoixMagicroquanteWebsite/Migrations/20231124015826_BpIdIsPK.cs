using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoixMagicroquanteWebsite.Migrations
{
    /// <inheritdoc />
    public partial class BpIdIsPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Basket_BPProductId",
                table: "BasketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BasketProduct",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "BasketProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BasketProduct_BasketId",
                table: "BasketProduct",
                column: "BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Basket_BasketId",
                table: "BasketProduct",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "BasketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Basket_BasketId",
                table: "BasketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct");

            migrationBuilder.DropIndex(
                name: "IX_BasketProduct_BasketId",
                table: "BasketProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BasketProduct");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "BasketProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProduct",
                table: "BasketProduct",
                column: "BPProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Basket_BPProductId",
                table: "BasketProduct",
                column: "BPProductId",
                principalTable: "Basket",
                principalColumn: "BasketId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
