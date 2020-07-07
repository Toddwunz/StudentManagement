using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Migrations
{
    public partial class addmoreSeedStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "ClassName", "Email", "Name" },
                values: new object[] { 2, 2, "XiaoWu@gmail.com", "Xiao Wu" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "ClassName", "Email", "Name" },
                values: new object[] { 3, 3, "RongRong@gmail.com", "Rong Rong" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
