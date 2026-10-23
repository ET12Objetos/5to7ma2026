using System.Data;
using Aplicacion.Contratos;
using Aplicacion.Servicios;
using MySqlConnector;
using Persistencia.Contratos;
using Persistencia.Repositorios;
using Presentacion.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("MySql");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Debe configurar ConnectionStrings:MySql en appsettings.json.");
}

builder.Services.AddScoped<IDbConnection>(_ => new MySqlConnection(connectionString));
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.EnableTryItOutByDefault();
    });
}

app.UseHttpsRedirection();

app.MapGet("/bienvenida", () =>
{
    return Results.Ok("Hola mundo!");
});

app.MapUsuarioEndpoints();

app.Run();

