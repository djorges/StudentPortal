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

builder.Services.AddScoped<CursoService>();
builder.Services.AddScoped<ProfesorService>();
builder.Services.AddScoped<DBUsuario>();
builder.Services.AddDbContext<DBMain>(options =>
    options.UseMySQL(connectionString)
);
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
