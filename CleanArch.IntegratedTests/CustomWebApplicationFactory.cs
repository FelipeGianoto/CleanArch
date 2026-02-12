using CleanArch.Domain.Entities;
using CleanArch.Infra.SqlServer.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.IntegratedTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private SqliteConnection? _connection;
        private readonly string _databaseName;

        public CustomWebApplicationFactory() : this(Guid.NewGuid().ToString("N"))
        {
        }

        public CustomWebApplicationFactory(string databaseName)
        {
            _databaseName = databaseName;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Remove quaisquer registros existentes relacionados ao AppDbContext
                var descriptors = services.Where(d =>
                    d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                    d.ServiceType == typeof(AppDbContext)).ToList();

                foreach (var d in descriptors) services.Remove(d);

                // Cria conexão SQLite InMemory nomeada (isolada por databaseName)
                // usa URI filename com mode=memory e cache=shared para permitir uso da mesma conexão
                _connection = new SqliteConnection($"Data Source=file:{_databaseName}?mode=memory&cache=shared");
                _connection.Open();

                // Registra DbContext usando SQLite e a conexão aberta
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                // Cria provider final
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                SeedDatabase(db);
            });
        }

        private static void SeedDatabase(AppDbContext db)
        {
            if (db.Products.Any())
                return;

            var category = new Category("Seed Category");
            db.Categories.Add(category);
            db.SaveChanges();

            db.Products.Add(new Product("Seed Product 1", "Desc", 10m, 5, null, category.Id));
            db.Products.Add(new Product("Seed Product 2", "Desc", 20m, 2, null, category.Id));

            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _connection?.Close();
                _connection?.Dispose();
            }
        }
    }
}
