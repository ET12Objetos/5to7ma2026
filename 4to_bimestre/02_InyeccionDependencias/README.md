# UsuariosApp: inyección de dependencias

API de ejemplo desarrollada con ASP.NET Core y .NET 10. La única entidad del proyecto es `Usuario`. Su endpoint, servicio y repositorio se organizan en tres capas, conectadas mediante interfaces e inyección de dependencias.

## Estructura del proyecto

```text
UsuariosApp.slnx
├── Presentacion/
│   ├── Endpoints/
│   │   └── UsuarioEndpoints.cs
│   └── Program.cs
├── Aplicacion/
│   ├── Contratos/
│   │   └── IUsuarioServicio.cs
│   └── Servicios/
│       └── UsuarioServicio.cs
└── Persistencia/
    ├── Contratos/
    │   └── IUsuarioRepositorio.cs
    ├── Entidades/
    │   └── Usuario.cs
    └── Repositorios/
        └── UsuarioRepositorio.cs
```

### Presentación: endpoint de Usuario

La capa `Presentacion` recibe las solicitudes HTTP y devuelve las respuestas de la API.

- `Program.cs` configura la aplicación, registra las dependencias y habilita las rutas.
- `Endpoints/UsuarioEndpoints.cs` agrupa las rutas de usuarios en el método de extensión `MapUsuarioEndpoints()`.
- El endpoint recibe `IUsuarioServicio` mediante `[FromServices]` y utiliza `Results.Ok(...)` para devolver una respuesta HTTP 200.

```csharp
public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
{
    app.MapGet("/usuario", ([FromServices] IUsuarioServicio usuarioServicio) =>
    {
        return Results.Ok(usuarioServicio.LeerUsuarios());
    });
}
```

El grupo de endpoints se incorpora en `Program.cs` con:

```csharp
app.MapUsuarioEndpoints();
```

### Aplicación: servicio de Usuario

La capa `Aplicacion` contiene `UsuarioServicio`, que coordina la consulta de usuarios.

- `Contratos/IUsuarioServicio.cs` define `LeerUsuarios()`, que devuelve `IReadOnlyList<Usuario>`.
- `Servicios/UsuarioServicio.cs` implementa esa interfaz y recibe `IUsuarioRepositorio` por constructor.
- Actualmente, `LeerUsuarios()` delega la consulta en `usuarioRepositorio.GetUsuarios()`.

```csharp
public class UsuarioServicio : IUsuarioServicio
{
    private readonly IUsuarioRepositorio usuarioRepositorio;

    public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio)
    {
        this.usuarioRepositorio = usuarioRepositorio;
    }

    public IReadOnlyList<Usuario> LeerUsuarios()
    {
        return usuarioRepositorio.GetUsuarios();
    }
}
```

### Persistencia: repositorio de Usuario

La capa `Persistencia` contiene `UsuarioRepositorio` y la única entidad del proyecto, `Usuario`.

- `Contratos/IUsuarioRepositorio.cs` define `GetUsuarios()`, que devuelve `IReadOnlyList<Usuario>`.
- `Repositorios/UsuarioRepositorio.cs` implementa el contrato y devuelve una lista fija de tres usuarios: Juan, Martin y Leonardo.

Aunque el proyecto referencia Dapper y MySqlConnector, el repositorio actual devuelve datos en memoria y no realiza consultas a una base de datos.

### Entidad Usuario

`Persistencia/Entidades/Usuario.cs` representa a cada usuario con estas propiedades:

| Propiedad | Tipo | Descripción |
| --- | --- | --- |
| `Id` | `int` | Identificador del usuario. |
| `Nombre` | `string` | Nombre del usuario. |
| `Email` | `string` | Correo electrónico del usuario. |

El repositorio y el servicio devuelven colecciones de esta misma entidad mediante `IReadOnlyList<Usuario>`.

## Registro de dependencias

`Presentacion/Program.cs` relaciona las interfaces con sus implementaciones antes de construir la aplicación:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddTransient<IUsuarioServicio, UsuarioServicio>();

var app = builder.Build();
```

`AddTransient` crea una instancia nueva cada vez que el contenedor resuelve la dependencia. Al resolver `IUsuarioServicio`, el contenedor construye un `UsuarioServicio` e inyecta un `UsuarioRepositorio` en su constructor.

Los endpoints se registran mediante el método de extensión; no necesitan un registro propio con `AddTransient`.

## Flujo de una consulta

```text
GET /usuario
    → UsuarioEndpoints
    → IUsuarioServicio / UsuarioServicio.LeerUsuarios()
    → IUsuarioRepositorio / UsuarioRepositorio.GetUsuarios()
    → Lista de usuarios
    → Respuesta HTTP 200 con JSON
```

El endpoint depende del contrato del servicio y el servicio depende del contrato del repositorio. El contenedor resuelve las implementaciones registradas en `Program.cs`.

## Endpoint de Usuario

| Método | Ruta | Ubicación | Respuesta |
| --- | --- | --- | --- |
| GET | `/usuario` | `Presentacion/Endpoints/UsuarioEndpoints.cs` | HTTP 200 con la lista de usuarios en JSON. |

Ejemplo de respuesta de `GET /usuario`:

```json
[
  { "id": 1, "nombre": "Juan", "email": "juan@mail.com" },
  { "id": 2, "nombre": "Martin", "email": "martin@mail.com" },
  { "id": 3, "nombre": "Leonardo", "email": "leonardo@mail.com" }
]
```

En el entorno `Development`, también se habilitan el documento OpenAPI en `/openapi/v1.json` y la interfaz Swagger UI en `/swagger` para explorar y probar los endpoints.
