using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AdminUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { 1, "admin@example.com", "$2a$11$Z7TsKPeQ8lgU7mdfKaOD0uMIMGGLCIaHEeKE.3FqaEjrcwJRB.zuu", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AdminUsers");
        }
    }
}
