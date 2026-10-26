using Aplicacion.Contratos;
using Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;
using Persistencia.Entidades;
using Persistencia.Repositorios;

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

        app.MapPut("/usuario/{id:guid}", (Guid id, UsuarioRequestDto usuarioDto, [FromServices] IUsuarioServicio usuarioServicio) =>
        {
            var usuarioResponse = usuarioServicio.ActualizarUsuario(id, usuarioDto);

            if (usuarioResponse is null)
                return Results.NotFound();

            return Results.Ok(usuarioResponse);
        });

        app.MapDelete("/usuario/{id:guid}", (Guid id, [FromServices] IUsuarioServicio usuarioServicio) =>
        {
            bool result = usuarioServicio.BorrarUsuario(id);

            if (!result)
                return Results.NotFound();

            return Results.NoContent();
        });
    }
}
