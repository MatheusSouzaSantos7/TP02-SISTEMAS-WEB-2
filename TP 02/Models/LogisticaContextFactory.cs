using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TP_02.Models
{
    public class LogisticaContextFactory : IDesignTimeDbContextFactory<LogisticaContext>
    {
        public LogisticaContext CreateDbContext(string[] args)
        {
            // Carrega o appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LogisticaContext>();

            // Lê a string de conexão
            var connectionString = configuration.GetConnectionString("LogisticaDB");

            builder.UseMySql(connectionString,new MySqlServerVersion(new Version(8, 0, 33)), mySqlOptions => mySqlOptions.EnableRetryOnFailure());


            return new LogisticaContext(builder.Options);
        }
    }
}
