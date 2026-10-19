using Aplicacion.Contratos;
using Aplicacion.Servicios;
using Persistencia.Contratos;
using Persistencia.Repositorios;
using Presentacion.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddTransient<IUsuarioServicio, UsuarioServicio>();

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

