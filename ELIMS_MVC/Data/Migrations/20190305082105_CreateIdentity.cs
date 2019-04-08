using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ELIMS_MVC.Migrations
{
    public partial class CreateIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CASNum = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    HasMSDS = table.Column<bool>(nullable: false),
                    HealthHazard = table.Column<int>(nullable: false),
                    FlammableHazard = table.Column<int>(nullable: false),
                    ReactiveHazard = table.Column<int>(nullable: false),
                    OtherHazard = table.Column<int>(nullable: false),
                    CurrentQty = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    IsCheckedOut = table.Column<bool>(nullable: false),
                    DateCheckedOut = table.Column<DateTime>(nullable: false),
                    DateReturned = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Consumed = table.Column<bool>(nullable: false),
                    HazWaste = table.Column<bool>(nullable: false),
                    Disposed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
