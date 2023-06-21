using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class AddImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("03b0f5f5-1690-40ea-8434-f3a4262eec2a"),
                columns: new[] { "Cost", "Description", "ImagePath", "Name" },
                values: new object[] { 949m, "Test1", "/images/Products/f5139353-92f2-4ebc-8585-9e887ae0de6e.jpg", "Кубик 3x3x3" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0d8762ae-47d3-45ef-9d89-14e96b674221"),
                columns: new[] { "Cost", "ImagePath", "Name" },
                values: new object[] { 499m, "/images/Products/edfaf6f0-ab7f-40cf-8794-d0d5e2610c84.jpg", "Кубик Брелок" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3b9a2b9b-70d0-41f7-80be-f1d028a19ce7"),
                columns: new[] { "Cost", "ImagePath", "Name" },
                values: new object[] { 799m, "/images/Products/c8d87138-9a03-4187-bb36-0bcb96d0ac28.jpg", "Кубик 2x2x2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("528f8578-a586-4b7b-855e-85dc9aad4423"),
                columns: new[] { "Cost", "ImagePath", "Name" },
                values: new object[] { 1299m, "/images/Products/090f4001-ea3e-4415-9b72-1c6b78321948.jpg", "Кубик 5x5x5" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("85520332-cc96-4f9e-9acf-ce583aa2954b"),
                columns: new[] { "Cost", "ImagePath", "Name" },
                values: new object[] { 1099m, "/images/Products/fea210b6-3c04-4dbd-b900-b24cf9468a43.jpg", "Кубик 4x4x4" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "ImagePath", "Name" },
                values: new object[] { new Guid("2305ac13-19d1-44e3-924d-4eefe68e8216"), 699m, "Test6", "/images/Products/0d1d34a7-d8ec-47fe-8ab1-23a512db9425.jpg", "Кубик Пирамида" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2305ac13-19d1-44e3-924d-4eefe68e8216"));

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("03b0f5f5-1690-40ea-8434-f3a4262eec2a"),
                columns: new[] { "Cost", "Description", "Name" },
                values: new object[] { 100m, "Test", "Kubik" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0d8762ae-47d3-45ef-9d89-14e96b674221"),
                columns: new[] { "Cost", "Name" },
                values: new object[] { 500m, "Kubik5" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3b9a2b9b-70d0-41f7-80be-f1d028a19ce7"),
                columns: new[] { "Cost", "Name" },
                values: new object[] { 200m, "Kubik2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("528f8578-a586-4b7b-855e-85dc9aad4423"),
                columns: new[] { "Cost", "Name" },
                values: new object[] { 300m, "Kubik3" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("85520332-cc96-4f9e-9acf-ce583aa2954b"),
                columns: new[] { "Cost", "Name" },
                values: new object[] { 400m, "Kubik4" });
        }
    }
}
