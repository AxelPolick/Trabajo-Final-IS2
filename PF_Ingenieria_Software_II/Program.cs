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
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        option.AccessDeniedPath = "/Usuario/Login";
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Connection String.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<PolideportivobdContext>(options => {
//    options.UseSqlServer(connectionString);
//});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PolideportivobdContext>(options => {
    options.UseSqlServer(connectionString, sqlServerOptionsAction => {
        sqlServerOptionsAction.CommandTimeout(30);
    });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<IDisciplinaService, DisciplinaService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<IDisciplinaTutorService, DisciplinaTutorService>();
builder.Services.AddScoped<IContratoService, ContratoService>();
builder.Services.AddScoped<ISesionService, SesionService>();
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IAlumnoBloqueService, AlumnoBloqueService>();
builder.Services.AddScoped<IBloqueService, BloqueService>();
builder.Services.AddScoped<IHorarioSesionService, HorarioSesionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

// Sesion var
app.UseAuthentication();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Landing}/{action=Index}/{id?}");

app.Run();
