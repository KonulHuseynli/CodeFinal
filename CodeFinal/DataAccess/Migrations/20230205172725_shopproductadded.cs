using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class shopproductadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_productCategories_ProductCategoryId",
                table: "ShopProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct");

            migrationBuilder.RenameTable(
                name: "ShopProduct",
                newName: "shopProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProduct_ProductCategoryId",
                table: "shopProducts",
                newName: "IX_shopProducts_ProductCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shopProducts",
                table: "shopProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_shopProducts_productCategories_ProductCategoryId",
                table: "shopProducts",
                column: "ProductCategoryId",
                principalTable: "productCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shopProducts_productCategories_ProductCategoryId",
                table: "shopProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shopProducts",
                table: "shopProducts");

            migrationBuilder.RenameTable(
                name: "shopProducts",
                newName: "ShopProduct");

            migrationBuilder.RenameIndex(
                name: "IX_shopProducts_ProductCategoryId",
                table: "ShopProduct",
                newName: "IX_ShopProduct_ProductCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_productCategories_ProductCategoryId",
                table: "ShopProduct",
                column: "ProductCategoryId",
                principalTable: "productCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
