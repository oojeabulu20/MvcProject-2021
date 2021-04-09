using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcShoes.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    ShoesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShoeImage = table.Column<string>(type: "varchar(100)",nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.ShoesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shoes");
        }
    }
}
