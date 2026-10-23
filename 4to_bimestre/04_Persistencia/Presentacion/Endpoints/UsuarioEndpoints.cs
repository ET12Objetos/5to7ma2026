using Aplicacion.Contratos;
using Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/usuario", ([FromServices] IUsuarioServicio usuarioServicio) =>
        {
            return Results.Ok(usuarioServicio.LeerUsuarios());
        });

        app.MapPost("/usuario", ([FromServices] IUsuarioServicio usuarioServicio, UsuarioRequestDto usuarioDto) =>
        {
            var usuarioResponse = usuarioServicio.CrearUsuario(usuarioDto);
            return Results.Created($"/usuario/{usuarioResponse.Id}", usuarioResponse);
        });
    }
}