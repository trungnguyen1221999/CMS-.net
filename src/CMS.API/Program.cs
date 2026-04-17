using CMS.API.Extensions;
using CMS.Data;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityService();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        await context.Database.OpenConnectionAsync();
        Console.WriteLine("✅ Connect supabase successfully");
        await context.Database.CloseConnectionAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ FULL ERROR:");
        Console.WriteLine(ex.ToString());
    }
}

await app.RunAsync();