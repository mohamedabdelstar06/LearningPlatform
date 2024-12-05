using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EnumAndIsTrending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTrending",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrending",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Courses");
        }
    }
}
