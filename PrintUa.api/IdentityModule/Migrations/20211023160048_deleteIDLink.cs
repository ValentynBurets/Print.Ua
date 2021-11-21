using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserIdentity.Migrations
{
    public partial class deleteIDLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IdLink",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "deb0ebd0-4274-432d-bd49-4afd6beebcca", "a66f77ca-a781-45a3-96da-159d019bfc33", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89212567-0a12-4632-b403-683e8b99c28e", "2a61e441-0024-414b-bbf0-383ff99c9da0", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "33ff6e7b-b2f9-477e-9e7c-445c0ac8c991", "8888aaef-bd29-4b79-a8c1-2c32118b4376", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33ff6e7b-b2f9-477e-9e7c-445c0ac8c991");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89212567-0a12-4632-b403-683e8b99c28e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "deb0ebd0-4274-432d-bd49-4afd6beebcca");

            migrationBuilder.AddColumn<Guid>(
                name: "IdLink",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
