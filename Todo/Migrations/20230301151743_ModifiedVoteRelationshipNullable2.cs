using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedVoteRelationshipNullable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentVotes_Profiles_ProfileID",
                table: "CommentVotes");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "CommentVotes",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentVotes_ProfileID",
                table: "CommentVotes",
                newName: "IX_CommentVotes_ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVotes_Profiles_ProfileId",
                table: "CommentVotes",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentVotes_Profiles_ProfileId",
                table: "CommentVotes");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "CommentVotes",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_CommentVotes_ProfileId",
                table: "CommentVotes",
                newName: "IX_CommentVotes_ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVotes_Profiles_ProfileID",
                table: "CommentVotes",
                column: "ProfileID",
                principalTable: "Profiles",
                principalColumn: "Id");
        }
    }
}
