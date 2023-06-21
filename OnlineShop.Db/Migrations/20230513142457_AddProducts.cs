using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class AddProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("03b0f5f5-1690-40ea-8434-f3a4262eec2a"), 100m, "Test", "Kubik" },
                    { new Guid("3b9a2b9b-70d0-41f7-80be-f1d028a19ce7"), 200m, "Test2", "Kubik2" },
                    { new Guid("528f8578-a586-4b7b-855e-85dc9aad4423"), 300m, "Test3", "Kubik3" },
                    { new Guid("85520332-cc96-4f9e-9acf-ce583aa2954b"), 400m, "Test4", "Kubik4" },
                    { new Guid("0d8762ae-47d3-45ef-9d89-14e96b674221"), 500m, "Test5", "Kubik5" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("03b0f5f5-1690-40ea-8434-f3a4262eec2a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0d8762ae-47d3-45ef-9d89-14e96b674221"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3b9a2b9b-70d0-41f7-80be-f1d028a19ce7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("528f8578-a586-4b7b-855e-85dc9aad4423"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("85520332-cc96-4f9e-9acf-ce583aa2954b"));
        }
    }
}
