using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Devagran.Migrations
{
    public partial class TableCommentsConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Publications_PublicatinId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "PublicatinId",
                table: "Comments",
                newName: "PublicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PublicatinId",
                table: "Comments",
                newName: "IX_Comments_PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Publications_PublicationId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "PublicationId",
                table: "Comments",
                newName: "PublicatinId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PublicationId",
                table: "Comments",
                newName: "IX_Comments_PublicatinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Publications_PublicatinId",
                table: "Comments",
                column: "PublicatinId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
