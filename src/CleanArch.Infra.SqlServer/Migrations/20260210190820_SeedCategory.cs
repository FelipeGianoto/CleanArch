using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO Categories (Name)
                VALUES 
                    ('Material Escolar'),
                    ('Eletrônicos'),
                    ('Acessórios');
             """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM Categories
                WHERE Name IN ('Material Escolar', 'Eletrônicos', 'Acessórios');
             """);
        }
    }
}
