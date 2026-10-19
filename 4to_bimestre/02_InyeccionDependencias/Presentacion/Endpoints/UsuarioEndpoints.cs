using Aplicacion.Contratos;
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
    }
}