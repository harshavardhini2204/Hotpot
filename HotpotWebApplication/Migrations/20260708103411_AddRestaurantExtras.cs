using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotpotWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantExtras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostForOne",
                table: "Restaurants",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTime",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Restaurants",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostForOne",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "DeliveryTime",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Restaurants");
        }
    }
}
