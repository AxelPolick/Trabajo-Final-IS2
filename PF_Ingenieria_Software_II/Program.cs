using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using PF_Ingenieria_Software_II.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Usuario/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Usuario/Login";
    });

//Connection String.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PolideportivobdContext>(options=>{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IRolService, RolService>();

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

//Sesion var
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Landing}/{action=Index}/{id?}");

app.Run();
