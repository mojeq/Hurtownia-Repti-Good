using Microsoft.EntityFrameworkCore.Migrations;

namespace HurtowniaReptiGood.Migrations
{
    public partial class addedfieldIdSubiektandIdGroupSubiektinProductstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdGroupSubiekt",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdSubiekt",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdGroupSubiekt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IdSubiekt",
                table: "Products");
        }
    }
}
