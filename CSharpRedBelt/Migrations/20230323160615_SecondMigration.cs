using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpRedBelt.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Creatorid",
                table: "Coupons",
                newName: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Coupons",
                newName: "Creatorid");
        }
    }
}
