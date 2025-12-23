using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tms.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintOnDriverCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DriverCities_DriverId",
                table: "DriverCities");

            migrationBuilder.CreateIndex(
                name: "IX_DriverCities_DriverId_Date",
                table: "DriverCities",
                columns: new[] { "DriverId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DriverCities_DriverId_Date",
                table: "DriverCities");

            migrationBuilder.CreateIndex(
                name: "IX_DriverCities_DriverId",
                table: "DriverCities",
                column: "DriverId");
        }
    }
}
