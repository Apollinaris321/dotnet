using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedVoteRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentVotes_Blogs_BlogId",
                table: "CommentVotes");

            migrationBuilder.DropIndex(
                name: "IX_CommentVotes_BlogId",
                table: "CommentVotes");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "CommentVotes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "CommentVotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_BlogId",
                table: "CommentVotes",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVotes_Blogs_BlogId",
                table: "CommentVotes",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }
    }
}
