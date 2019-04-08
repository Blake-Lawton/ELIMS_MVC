using Microsoft.EntityFrameworkCore.Migrations;

namespace ELIMS_MVC.Migrations
{
    public partial class ContactUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactDate",
                table: "ContactForm",
                nullable: true,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactDate",
                table: "ContactForm");
        }
    }
}
