using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Migrations
{
    /// <inheritdoc />
    public partial class AddedBlogVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "CommentVotes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BlogVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileID = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogVotes_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogVotes_Profiles_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_BlogId",
                table: "CommentVotes",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogVotes_BlogId",
                table: "BlogVotes",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogVotes_ProfileID",
                table: "BlogVotes",
                column: "ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVotes_Blogs_BlogId",
                table: "CommentVotes",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentVotes_Blogs_BlogId",
                table: "CommentVotes");

            migrationBuilder.DropTable(
                name: "BlogVotes");

            migrationBuilder.DropIndex(
                name: "IX_CommentVotes_BlogId",
                table: "CommentVotes");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "CommentVotes");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Blogs");
        }
    }
}
