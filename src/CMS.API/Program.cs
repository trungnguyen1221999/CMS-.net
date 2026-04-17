using CMS.API.Extensions;
using CMS.Data;
using Microsoft.EntityFrameworkCore;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Connect DB
// 1. Thêm dòng này để fix lỗi múi giờ của Postgres (rất quan trọng)
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

//Config ASP.NET Core Identity
builder.Services.AddIdentityService();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Chèn vào cuối file Program.cs, trước app.Run();
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    AppDbContext context = services.GetRequiredService<AppDbContext>();

    try
    {
        // Thay CanConnect bằng cái này - nó sẽ throw nếu lỗi
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

