using Microsoft.EntityFrameworkCore.Migrations;

namespace LicenseKeyCore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblDataKeys",
                columns: table => new
                {
                    DataKeysId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(nullable: true),
                    LicenseType = table.Column<int>(nullable: false),
                    ExpereienceDays = table.Column<int>(nullable: false),
                    ProducKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDataKeys", x => x.DataKeysId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblDataKeys");
        }
    }
}
