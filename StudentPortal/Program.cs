using StudentPortal.Data;
using StudentPortal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using StudentPortal.Mappers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<UtilService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<EstudianteService>();
builder.Services.AddScoped<ExamenService>();
builder.Services.AddScoped<CursoService>();
builder.Services.AddScoped<ProfesorService>();

//Configuring Session Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.IOTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.Name = ".StudentPortal.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Path = "/";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
//
builder.Services.AddDbContext<DBMain>(options =>
    options.UseMySQL(connectionString)
);
//
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo{
        Version = "v1",
        Title = "API de Student Portal",
        Description = "Documentación de la API para el proyecto Student Portal"
    });
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        return apiDesc.ActionDescriptor.EndpointMetadata
            .Any(em => em is ApiControllerAttribute);
    });
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Student Portal v1");
    //c.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
