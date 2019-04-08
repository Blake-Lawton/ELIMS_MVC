using Microsoft.EntityFrameworkCore.Migrations;

namespace ELIMS_MVC.Migrations
{
    public partial class Groupmembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupMembers",
                table: "Request",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupMembers",
                table: "Request");
        }
    }
}
