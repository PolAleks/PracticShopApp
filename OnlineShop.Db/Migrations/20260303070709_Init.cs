using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.cart_id);
                });

            migrationBuilder.CreateTable(
                name: "comparisons",
                columns: table => new
                {
                    comparison_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comparisons", x => x.comparison_id);
                });

            migrationBuilder.CreateTable(
                name: "delivery_user",
                columns: table => new
                {
                    delivery_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_user", x => x.delivery_user_id);
                });

            migrationBuilder.CreateTable(
                name: "favorites",
                columns: table => new
                {
                    favorite_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorites", x => x.favorite_id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    description = table.Column<string>(type: "character varying(4069)", maxLength: 4069, nullable: true),
                    photo_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    creation_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    DeliveryUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_order_delivery_user_DeliveryUserId",
                        column: x => x.DeliveryUserId,
                        principalTable: "delivery_user",
                        principalColumn: "delivery_user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comparison_products",
                columns: table => new
                {
                    comparison_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comparison_products", x => new { x.comparison_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_comparison_products_comparisons_comparison_id",
                        column: x => x.comparison_id,
                        principalTable: "comparisons",
                        principalColumn: "comparison_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comparison_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favorite_products",
                columns: table => new
                {
                    favorite_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorite_products", x => new { x.favorite_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_favorite_products_favorites_favorite_id",
                        column: x => x.favorite_id,
                        principalTable: "favorites",
                        principalColumn: "favorite_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favorite_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    cart_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    cart_id = table.Column<Guid>(type: "uuid", nullable: true),
                    order_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_items", x => x.cart_item_id);
                    table.ForeignKey(
                        name: "FK_cart_items_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "cart_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cart_items_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "product_id", "cost", "description", "name", "photo_path" },
                values: new object[,]
                {
                    { 1, 1500m, "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", "Товар 1", "/img/product.png" },
                    { 2, 2000m, "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", "Товар 2", "/img/product.png" },
                    { 3, 1300m, "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", "Товар 3", "/img/product.png" },
                    { 4, 3000m, "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", "Товар 4", "/img/product.png" },
                    { 5, 1400m, "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", "Товар 5", "/img/product.png" },
                    { 6, 3060m, "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", "Товар 6", "/img/product.png" },
                    { 7, 2800m, "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", "Товар 7", "/img/product.png" },
                    { 8, 500m, "Lorem ipsum dolor sit amet. Do est aliquip nostrud qui nisi adipiscing commodo culpa dolor culpa.", "Товар 7", "/img/product.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_cart_id",
                table: "cart_items",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_order_id",
                table: "cart_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_product_id",
                table: "cart_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_comparison_products_product_id",
                table: "comparison_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_favorite_products_product_id",
                table: "favorite_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_DeliveryUserId",
                table: "order",
                column: "DeliveryUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "comparison_products");

            migrationBuilder.DropTable(
                name: "favorite_products");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "comparisons");

            migrationBuilder.DropTable(
                name: "favorites");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "delivery_user");
        }
    }
}
