using Microsoft.EntityFrameworkCore.Migrations;

namespace OpetNet.Infra.Data.Migrations
{
    public partial class addCustomerFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImgProfile",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlProfile",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImgProfile",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UrlProfile",
                table: "Customers");
        }
    }
}
