using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NebulaTextbook.Data;
using NebulaTextbook.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<NebulaTextbookContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("NebulaTextbookContext") ?? throw new InvalidOperationException("Connection string 'NebulaTextbookContext' not found.")));
var app = builder.Build();

// Add Controllers
app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Upon service creation, check for empty database
// If empty -> seed with test data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbSeed = new DatabaseSeed();
    dbSeed.InitializeDB(services);
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Assets")
    // Or some other absolute path. 
    )
});

app.Run();
