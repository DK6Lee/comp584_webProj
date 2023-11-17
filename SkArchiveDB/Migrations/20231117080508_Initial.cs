using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkArchiveDB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    iso2 = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    iso3 = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    pic = table.Column<byte[]>(type: "image", nullable: false),
                    brand_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_brands",
                        column: x => x.id,
                        principalTable: "brands",
                        principalColumn: "id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
