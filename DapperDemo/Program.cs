using DapperDemo.Data;
using DapperDemo.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/// <summary>
///  Add the DbContext
/// </summary>

/// Get connection string
// Step 2 - add the ApplicationDbContext - DefaultConnection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

/// Step 3 - add the ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddScoped<ICompanyRepository, CompanyRepositoryEF>(); // This one uses the Interface

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>(); // To use the dapper in action

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
