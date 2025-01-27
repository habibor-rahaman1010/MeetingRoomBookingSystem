using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations.MRBSDb
{
    /// <inheritdoc />
    public partial class UpdateMeetingRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRCodeData",
                table: "MeetingRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "MeetingRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeData",
                table: "MeetingRooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MeetingRooms");
        }
    }
}
