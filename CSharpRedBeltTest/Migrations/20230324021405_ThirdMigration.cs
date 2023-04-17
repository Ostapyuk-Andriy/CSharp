using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpRedBelt.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Coupons");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CreatorId",
                table: "Coupons",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Users_CreatorId",
                table: "Coupons",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Users_CreatorId",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_CreatorId",
                table: "Coupons");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Coupons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
