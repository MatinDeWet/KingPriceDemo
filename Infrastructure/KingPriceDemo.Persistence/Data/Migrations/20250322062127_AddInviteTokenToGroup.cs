using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingPriceDemo.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInviteTokenToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InviteToken",
                table: "Group",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteToken",
                table: "Group");
        }
    }
}
