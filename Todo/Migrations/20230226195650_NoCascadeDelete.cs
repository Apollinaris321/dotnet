using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Migrations
{
    /// <inheritdoc />
    public partial class NoCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Profiles_ProfileId",
                table: "Blogs");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Profiles_ProfileId",
                table: "Blogs",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Profiles_ProfileId",
                table: "Blogs");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Profiles_ProfileId",
                table: "Blogs",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
