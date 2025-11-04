using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2_AP1_JesusBonilla.Migrations
{
    /// <inheritdoc />
    public partial class CambiosModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Importe",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Importe",
                table: "Pedidos");
        }
    }
}
