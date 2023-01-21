using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBlogBe.Migrations
{
    public partial class RenameCoverImageToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Posts",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Posts",
                newName: "CoverImage");
        }
    }
}
