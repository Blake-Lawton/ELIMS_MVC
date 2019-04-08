using Microsoft.EntityFrameworkCore.Migrations;

namespace ELIMS_MVC.Migrations
{
    public partial class UpdateRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "Request");

            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Request",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Request");

            migrationBuilder.AddColumn<string>(
                name: "RequestStatus",
                table: "Request",
                nullable: true,
                defaultValue: "Pending");
        }
    }
}
