using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventVault.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventAndVenueFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove Columns from Venuetable that aren't in use
            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Venues");

            // Add missing columns for the Event table
            migrationBuilder.AddColumn<string>(
                name: "EventId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "APIEventUrlPage",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TicketsRelease",
                table: "Events",
                nullable: true);


            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Venues",
                nullable: true);

            // Add missing columns for the Venue table
            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Venues",
                nullable: true);



            migrationBuilder.AddColumn<string>(
                name: "LocationLat",
                table: "Venues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationLong",
                table: "Venues",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the columns if the migration is rolled back
            migrationBuilder.DropColumn(name: "EventId", table: "Events");
            migrationBuilder.DropColumn(name: "Category", table: "Events");
            migrationBuilder.DropColumn(name: "Date", table: "Events");
            migrationBuilder.DropColumn(name: "ImageUrl", table: "Events");
            migrationBuilder.DropColumn(name: "APIEventUrlPage", table: "Events");
            migrationBuilder.DropColumn(name: "EventUrlPage", table: "Events");
            migrationBuilder.DropColumn(name: "TicketsRelease", table: "Events");
            migrationBuilder.DropColumn(name: "HighestPrice", table: "Events");
            migrationBuilder.DropColumn(name: "LowestPrice", table: "Events");
            migrationBuilder.DropColumn(name: "ZipCode", table: "Venues");
            migrationBuilder.DropColumn(name: "City", table: "Venues");
            migrationBuilder.DropColumn(name: "LocationLat", table: "Venues");
            migrationBuilder.DropColumn(name: "LocationLong", table: "Venues");
        }
    }
}
