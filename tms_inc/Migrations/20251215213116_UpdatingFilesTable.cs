using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tms.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingFilesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUploaded",
                table: "PersonFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUploaded",
                table: "PersonFiles");
        }
    }
}
