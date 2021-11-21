using Microsoft.EntityFrameworkCore.Migrations;

namespace UserIdentity.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "244f26ad-035f-4dc0-b98a-6b413d344b00", "b19fd3cd-0424-440f-a32c-71bcda1756a0", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a136754-42f2-4309-a428-250ef30b9bd3", "b18e4808-3243-46d7-9996-89197defa162", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc31fc71-f007-42fd-8104-4f4eb01fdb4a", "ced17093-5bed-4362-959b-250618fddfe2", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a136754-42f2-4309-a428-250ef30b9bd3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "244f26ad-035f-4dc0-b98a-6b413d344b00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc31fc71-f007-42fd-8104-4f4eb01fdb4a");
        }
    }
}
