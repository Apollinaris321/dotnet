using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Blogs",
                newName: "ProfileId");

            migrationBuilder.AddColumn<long>(
                name: "ProfileId",
                table: "Comments",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Blogs",
                newName: "AuthorId");

            migrationBuilder.AddColumn<long>(
                name: "AuthorId",
                table: "Comments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
