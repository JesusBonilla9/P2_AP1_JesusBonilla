using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2_AP1_JesusBonilla.Migrations
{
    /// <inheritdoc />
    public partial class CambiosModels1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Precio",
                table: "PedidosDetalles",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Precio",
                table: "PedidosDetalles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
