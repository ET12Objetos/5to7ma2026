using _01_MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
     options.SwaggerEndpoint("/openapi/v1.json", "v1"));
}

List<Alumno> alumnos = new List<Alumno>();

alumnos.Add(new Alumno { Nombre = "Jose", Edad = 20 });
alumnos.Add(new Alumno { Nombre = "Juan", Edad = 30 });

app.MapGet("/alumno", () =>
{
    return Results.Ok(alumnos);
});

app.UseHttpsRedirection();


app.Run();