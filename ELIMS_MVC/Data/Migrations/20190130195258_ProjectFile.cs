using Microsoft.EntityFrameworkCore.Migrations;

namespace ELIMS_MVC.Migrations
{
    public partial class ProjectFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Request",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactID",
                table: "Request",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectFile",
                table: "Request",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectFile",
                table: "Request");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Request",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ContactID",
                table: "Request",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
