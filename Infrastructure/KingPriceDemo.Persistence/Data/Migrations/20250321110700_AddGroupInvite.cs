using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingPriceDemo.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupInvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupInvite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    AcceptedByUserId = table.Column<int>(type: "int", nullable: true),
                    DateTimeAccepted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateTimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupInvite_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupInvite_User_AcceptedByUserId",
                        column: x => x.AcceptedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvite_AcceptedByUserId",
                table: "GroupInvite",
                column: "AcceptedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvite_GroupId",
                table: "GroupInvite",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupInvite");
        }
    }
}
