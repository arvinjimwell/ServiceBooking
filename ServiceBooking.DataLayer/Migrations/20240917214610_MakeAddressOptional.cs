using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceBooking.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class MakeAddressOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformation_Address_AddressId",
                table: "ContactInformation");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "ContactInformation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformation_Address_AddressId",
                table: "ContactInformation",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformation_Address_AddressId",
                table: "ContactInformation");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "ContactInformation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformation_Address_AddressId",
                table: "ContactInformation",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
