using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Migrations
{
    /// <inheritdoc />
    public partial class AddedBlogVotesAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogVotes_Profiles_ProfileID",
                table: "BlogVotes");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "BlogVotes",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogVotes_ProfileID",
                table: "BlogVotes",
                newName: "IX_BlogVotes_ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogVotes_Profiles_ProfileId",
                table: "BlogVotes",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogVotes_Profiles_ProfileId",
                table: "BlogVotes");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "BlogVotes",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_BlogVotes_ProfileId",
                table: "BlogVotes",
                newName: "IX_BlogVotes_ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogVotes_Profiles_ProfileID",
                table: "BlogVotes",
                column: "ProfileID",
                principalTable: "Profiles",
                principalColumn: "Id");
        }
    }
}
