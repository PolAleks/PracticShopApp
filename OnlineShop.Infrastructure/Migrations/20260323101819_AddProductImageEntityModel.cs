using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImageEntityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_image",
                columns: table => new
                {
                    product_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    image_path = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_image", x => x.product_image_id);
                    table.ForeignKey(
                        name: "FK_product_image_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_image_product_id_is_main",
                table: "product_image",
                columns: new[] { "product_id", "is_main" },
                unique: true,
                filter: "\"is_main\" = true");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_image");
        }
    }
}
