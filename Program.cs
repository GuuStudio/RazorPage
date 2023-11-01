using Album.Data;
using Album.Model;
using Album.Models;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOptions();

builder.Services.AddDbContext<AppDbContext>( options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext"));
});

builder.Services.AddRazorPages();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();


// dotnet aspnet-codegenerator razorpage -m Article -dc AppDbContext -udl -outDir Pages/Blog --referenceScriptLibraries