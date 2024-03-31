using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParentCompanyModel.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParentCompany",
                columns: table => new
                {
                    ParentCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Table__026F1F3D9BABDD8E", x => x.ParentCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Subsidiary",
                columns: table => new
                {
                    SubsidiaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Location = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Revenue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ParentCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Table__5E27442B633F7B59", x => x.SubsidiaryId);
                    table.ForeignKey(
                        name: "FK_Subsidiary_ParentCompany",
                        column: x => x.ParentCompanyId,
                        principalTable: "ParentCompany",
                        principalColumn: "ParentCompanyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiary_ParentCompanyId",
                table: "Subsidiary",
                column: "ParentCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subsidiary");

            migrationBuilder.DropTable(
                name: "ParentCompany");
        }
    }
}
