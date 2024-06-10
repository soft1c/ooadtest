using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rent.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracija3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Nekretnina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Nekretnina");
        }
    }
}
