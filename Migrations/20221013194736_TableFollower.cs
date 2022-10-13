using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Devagran.Migrations
{
    public partial class TableFollower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowerId = table.Column<int>(type: "int", nullable: true),
                    FollowedId = table.Column<int>(type: "int", nullable: true),
                    Follower_User_Id = table.Column<int>(type: "int", nullable: false),
                    Followed_User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Followers_Users_Followed_User_Id",
                        column: x => x.Followed_User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Followers_Users_Follower_User_Id",
                        column: x => x.Follower_User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Followers_Followed_User_Id",
                table: "Followers",
                column: "Followed_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_Follower_User_Id",
                table: "Followers",
                column: "Follower_User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Followers");
        }
    }
}
