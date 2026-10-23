# UsuariosApp: DTO, GUID e inyección de dependencias

API de ejemplo desarrollada con ASP.NET Core y .NET 10. Permite consultar y crear usuarios mediante GET y POST. Los usuarios se identifican con GUID y la API intercambia datos mediante DTO. Sus tres capas se conectan mediante interfaces e inyección de dependencias.

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
│   ├── DTOs/
│   │   └── UsuarioDto.cs
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

## Responsabilidad de cada capa

| Capa | Responsabilidad |
| --- | --- |
| `Presentacion` | Recibe solicitudes HTTP y devuelve respuestas. `Program.cs` configura la aplicación y `UsuarioEndpoints.cs` define las rutas. |
| `Aplicacion` | Define los DTO y coordina las operaciones mediante `UsuarioServicio`. Convierte DTO a entidades y entidades a DTO. |
| `Persistencia` | Define la entidad `Usuario` y el repositorio que consulta y agrega usuarios en una lista en memoria. |

El endpoint depende de `IUsuarioServicio` y el servicio depende de `IUsuarioRepositorio`. El contenedor resuelve las implementaciones registradas en `Program.cs`.

## ¿Qué es un DTO?

Un **DTO (Data Transfer Object)** es un objeto que transporta datos entre capas o entre el cliente y la API. Permite definir qué datos recibe y devuelve cada operación sin exponer directamente la entidad de persistencia.

En `Aplicacion/DTOs/UsuarioDto.cs` se definen dos DTO:

```csharp
public class UsuarioRequestDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UsuarioResponseDto : UsuarioRequestDto
{
    public Guid Id { get; set; }
}
```

| Tipo | Uso | Propiedades |
| --- | --- | --- |
| `UsuarioRequestDto` | Datos que envía el cliente al crear un usuario. | `Nombre`, `Email`. |
| `UsuarioResponseDto` | Datos que devuelve la API al consultar o crear usuarios. | `Id`, `Nombre`, `Email`. |

En este ejemplo, `UsuarioResponseDto` hereda de `UsuarioRequestDto`: reutiliza `Nombre` y `Email` y agrega `Id`. El cliente no necesita enviar un identificador al crear un usuario porque lo genera el servicio.

La entidad `Usuario` representa los datos que maneja el repositorio. Los DTO representan el contrato de entrada y salida de la API. Aunque algunas propiedades coincidan, sus responsabilidades son distintas. Por ejemplo, se podría agregar una propiedad interna a la entidad sin incluirla en la respuesta pública.

### Conversión entre DTO y entidad

`UsuarioServicio` recibe `IUsuarioRepositorio` por constructor. Su método `CrearUsuario()` convierte el DTO de entrada en una entidad, genera su identificador, la agrega al repositorio y devuelve un DTO de respuesta:

```csharp
public UsuarioResponseDto CrearUsuario(UsuarioRequestDto usuarioDto)
{
    // DTO de entrada → entidad.
    Usuario usuario = new Usuario
    {
        Id = Guid.NewGuid(),
        Nombre = usuarioDto.Nombre,
        Email = usuarioDto.Email
    };

    usuarioRepositorio.CreateUsuario(usuario);

    // Entidad → DTO de respuesta.
    return new UsuarioResponseDto
    {
        Id = usuario.Id,
        Nombre = usuario.Nombre,
        Email = usuario.Email
    };
}
```

Para la consulta, `LeerUsuarios()` obtiene las entidades del repositorio y utiliza `Select` para convertir cada una en un `UsuarioResponseDto`.

El contrato del servicio es:

```csharp
public interface IUsuarioServicio
{
    UsuarioResponseDto CrearUsuario(UsuarioRequestDto usuarioDto);
    IReadOnlyList<UsuarioResponseDto> LeerUsuarios();
}
```

El repositorio trabaja con entidades `Usuario`: `GetUsuarios()` devuelve `IReadOnlyList<Usuario>` y `CreateUsuario(Usuario usuario)` agrega una entidad a la lista. El servicio devuelve DTO a la capa de presentación.

## Entidad Usuario e identificadores GUID

`Persistencia/Entidades/Usuario.cs` representa a cada usuario con estas propiedades:

| Propiedad | Tipo | Descripción |
| --- | --- | --- |
| `Id` | `Guid` | Identificador del usuario. |
| `Nombre` | `string` | Nombre del usuario. |
| `Email` | `string` | Correo electrónico del usuario. |

Un **GUID (Globally Unique Identifier)** es un identificador de 128 bits. En C# se representa con el tipo `Guid`; en el JSON de la API se serializa como una cadena con este formato:

```text
f47ac10b-58cc-4372-a567-0e02b2c3d479
```

Para generar un identificador nuevo se utiliza:

```csharp
Id = Guid.NewGuid();
```

Cada llamada genera un nuevo GUID con una probabilidad de repetición extremadamente baja. No es un número entero correlativo.

`new Guid()` no genera un identificador aleatorio: devuelve el valor vacío, equivalente a `Guid.Empty`:

```text
00000000-0000-0000-0000-000000000000
```

Actualmente, la propiedad `Id` de la entidad tiene el inicializador `new Guid()`. Los usuarios iniciales del repositorio y el método `CrearUsuario()` asignan explícitamente `Guid.NewGuid()`, por lo que reemplazan ese valor vacío al crear esos usuarios.

## Registro de dependencias y almacenamiento

`Presentacion/Program.cs` relaciona las interfaces con sus implementaciones antes de construir la aplicación:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddTransient<IUsuarioServicio, UsuarioServicio>();

var app = builder.Build();
```

`AddTransient` crea una instancia nueva cada vez que el contenedor resuelve la dependencia. Al resolver `IUsuarioServicio`, el contenedor construye un `UsuarioServicio` e inyecta un `UsuarioRepositorio` en su constructor.

El repositorio inicializa una lista con Juan, Martin y Leonardo, cada uno con un GUID generado. Aunque el proyecto referencia Dapper y MySqlConnector, la implementación actual trabaja en memoria y no utiliza una base de datos.

Como la lista pertenece a cada instancia del repositorio y este está registrado como transitorio, cada nueva instancia comienza con los tres usuarios iniciales y nuevos GUID. Un usuario agregado por POST no se conserva para un GET posterior que resuelva otra instancia del repositorio. El ejemplo actual no ofrece persistencia entre solicitudes.

## Endpoints de Usuario

`Presentacion/Endpoints/UsuarioEndpoints.cs` agrupa las rutas en el método de extensión `MapUsuarioEndpoints()`:

```csharp
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
```

Los endpoints reciben el servicio mediante `[FromServices]`. Se incorporan en `Program.cs` con:

```csharp
app.MapUsuarioEndpoints();
```

No necesitan un registro propio con `AddTransient`.

| Método | Ruta | Entrada | Respuesta |
| --- | --- | --- | --- |
| GET | `/usuario` | Sin cuerpo. | HTTP 200 con una lista de `UsuarioResponseDto`. |
| POST | `/usuario` | JSON con los datos de `UsuarioRequestDto`. | HTTP 201 con el `UsuarioResponseDto` creado y el encabezado `Location`. |

### GET /usuario

Consulta los usuarios y devuelve una lista de DTO de respuesta en JSON.

```text
GET /usuario
    → UsuarioEndpoints
    → UsuarioServicio.LeerUsuarios()
    → UsuarioRepositorio.GetUsuarios()
    → Lista de entidades Usuario
    → Conversión a UsuarioResponseDto en el servicio
    → Respuesta HTTP 200 con JSON
```

Ejemplo de respuesta:

```json
[
  { "id": "f47ac10b-58cc-4372-a567-0e02b2c3d479", "nombre": "Juan", "email": "juan@mail.com" },
  { "id": "9b2d6e31-4c8f-4a70-9d12-8f0e3b6c5a24", "nombre": "Martin", "email": "martin@mail.com" },
  { "id": "2c7e8a95-1f34-4d6b-a809-5e2c0f713b68", "nombre": "Leonardo", "email": "leonardo@mail.com" }
]
```

Los GUID de los ejemplos son ilustrativos; los valores reales se generan al crear cada usuario.

### POST /usuario

El método HTTP **POST** se utiliza aquí para crear un usuario. El cliente envía los datos en el cuerpo de la solicitud con `Content-Type: application/json`. ASP.NET Core convierte ese JSON en un `UsuarioRequestDto`.

Ejemplo de solicitud:

```http
POST /usuario
Content-Type: application/json

{
  "nombre": "Ana",
  "email": "ana@mail.com"
}
```

El cliente envía `nombre` y `email`; el servicio genera el `id`. El flujo es:

```text
POST /usuario con JSON
    → UsuarioEndpoints recibe UsuarioRequestDto
    → UsuarioServicio.CrearUsuario()
    → Conversión del DTO a entidad Usuario y generación del GUID
    → UsuarioRepositorio.CreateUsuario() agrega la entidad a la lista
    → El servicio convierte la entidad a UsuarioResponseDto
    → Respuesta HTTP 201 Created con JSON y Location
```

Ejemplo de respuesta:

```http
HTTP/1.1 201 Created
Content-Type: application/json
Location: /usuario/6a1d9c42-3e75-4b80-92f6-7c0e5a18d934

{
  "id": "6a1d9c42-3e75-4b80-92f6-7c0e5a18d934",
  "nombre": "Ana",
  "email": "ana@mail.com"
}
```

`Results.Created(...)` establece el estado **201 Created**, incluye el objeto de respuesta en el cuerpo y coloca la ruta indicada en el encabezado `Location`. El código actual devuelve `/usuario/{id}` como ubicación del recurso, pero todavía no implementa un endpoint GET para consultar un usuario por su identificador.

### Probar desde Swagger

En el entorno `Development`, se habilitan el documento OpenAPI en `/openapi/v1.json` y la interfaz Swagger UI en `/swagger` para explorar y probar los endpoints.

Para probar la creación, abrir `POST /usuario`, completar el cuerpo con `nombre` y `email` y ejecutar la solicitud. La respuesta debe mostrar el estado 201 y un GUID generado en la propiedad `id`.
