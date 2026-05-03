using DMMS.WebMVC.App.Data;
using DMMS.WebMVC.App.Repositories;
using DMMS.WebMVC.App.Services;
using Microsoft.AspNetCore.Http; // Add this at the top
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register DbContext
//builder.Services.AddDbContext<StockPilotWebAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StockPilotWebAppContext")));

builder.Services.AddControllersWithViews(); // Registers MVC

builder.Services.AddRazorPages(); // Only if you're using Razor Pages too
builder.Services.AddDbContext<DMMSWebMVCAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DMMSWebMVCAppContext") ?? throw new InvalidOperationException("Connection string 'DMMSWebMVCAppContext' not found.")));
// Add session services
builder.Services.AddDistributedMemoryCache(); // Use in-memory store

builder.Services.AddControllersWithViews()
        .AddRazorRuntimeCompilation();

builder.Services.AddSingleton<IDbContextFactory, DbContextFactory>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;


    //options.IdleTimeout = TimeSpan.FromHours(12); // How long session data is kept
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
    //options.Cookie.MaxAge = TimeSpan.FromHours(12); // Persistent cookie lifetime
});

// 3️⃣ Add IHttpContextAccessor service
builder.Services.AddHttpContextAccessor();

// Add Authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Identity/Login";
        options.AccessDeniedPath = "/Identity/AccessDenied";
    });

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Enables serving static files from wwwroot

app.UseRouting();

app.UseSession();

app.UseAuthentication(); // Important

app.UseAuthorization();

// Set default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();