using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace CMS.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CMS.API"))
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(config.GetConnectionString("MigrationConnection"));

        return new AppDbContext(optionsBuilder.Options);
    }
}