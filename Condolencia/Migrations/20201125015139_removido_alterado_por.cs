using Microsoft.EntityFrameworkCore.Migrations;

namespace Condolencia.Migrations
{
    public partial class removido_alterado_por : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAlteradoPor",
                table: "MensagemModerada");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAlteradoPor",
                table: "MensagemModerada",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
