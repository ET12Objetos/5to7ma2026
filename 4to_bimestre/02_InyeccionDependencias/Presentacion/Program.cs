var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

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

app.Run();

