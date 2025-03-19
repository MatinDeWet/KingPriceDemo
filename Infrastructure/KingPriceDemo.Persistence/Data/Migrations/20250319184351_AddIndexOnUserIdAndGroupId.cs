using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingPriceDemo.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexOnUserIdAndGroupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId",
                table: "UserGroup",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGroup_UserId",
                table: "UserGroup");
        }
    }
}
