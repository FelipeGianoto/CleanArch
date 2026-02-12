using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO Products
                    (Name, Description, Price, Stock, Image, CategoryId)
                VALUES
                    (
                        'Caderno Universitário',
                        'Caderno universitário 200 folhas capa dura',
                        29.90,
                        100,
                        'caderno-universitario.jpg',
                        1
                    ),
                    (
                        'Mouse Sem Fio',
                        'Mouse sem fio USB com design ergonômico',
                        89.90,
                        50,
                        'mouse-sem-fio.jpg',
                        2
                    );
             """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM Products
                WHERE Name IN ('Caderno Universitário', 'Mouse Sem Fio');
             """);
        }
    }
}
