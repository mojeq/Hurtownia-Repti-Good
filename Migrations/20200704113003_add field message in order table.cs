using Microsoft.EntityFrameworkCore.Migrations;

namespace HurtowniaReptiGood.Migrations
{
    public partial class addfieldmessageinordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderMessage",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderMessage",
                table: "Orders");
        }
    }
}
