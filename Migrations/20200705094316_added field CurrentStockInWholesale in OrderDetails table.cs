using Microsoft.EntityFrameworkCore.Migrations;

namespace HurtowniaReptiGood.Migrations
{
    public partial class addedfieldCurrentStockInWholesaleinOrderDetailstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentStockInWholesale",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStockInWholesale",
                table: "OrderDetails");
        }
    }
}
