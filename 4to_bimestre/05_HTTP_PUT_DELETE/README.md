# UsuariosApp: Dapper y persistencia en MySQL

API de ejemplo desarrollada con ASP.NET Core y .NET 10. Permite consultar, crear, actualizar y borrar usuarios mediante GET, POST, PUT y DELETE, con persistencia en MySQL mediante Dapper. El borrado es lógico (Soft Delete): conserva el registro en la base de datos y lo oculta de las consultas. Sus tres capas se conectan mediante interfaces e inyección de dependencias.

## Estructura del proyecto

```text
.
├── UsuariosApp.slnx
├── database.sql
├── Presentacion/
│   ├── Endpoints/
│   │   └── UsuarioEndpoints.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
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
| `Aplicacion` | Coordina la consulta, creación, actualización y borrado mediante `UsuarioServicio` y el contrato del repositorio. |
| `Persistencia` | Define la entidad `Usuario` y el repositorio que consulta, agrega, actualiza y marca usuarios como borrados en MySQL mediante Dapper. |

El endpoint depende de `IUsuarioServicio` y el servicio depende de `IUsuarioRepositorio`. El contenedor resuelve las implementaciones registradas en `Program.cs`.

## Integración del repositorio con Dapper

`Persistencia/Persistencia.csproj` referencia los paquetes `Dapper` y `MySqlConnector`. Dapper ejecuta el SQL y convierte las filas en objetos C#; MySqlConnector proporciona `MySqlConnection`, la conexión con MySQL.

`UsuarioRepositorio` recibe una `IDbConnection` por constructor. La conexión concreta se configura en `Program.cs`, por lo que el repositorio puede concentrarse en las consultas:

```csharp
using System.Data;
using Dapper;
using Persistencia.Contratos;
using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly IDbConnection conexion;

    public UsuarioRepositorio(IDbConnection conexion)
    {
        this.conexion = conexion;
    }

    public void CreateUsuario(Usuario usuario)
    {
        const string sql = """
            INSERT INTO Usuarios (Id, Nombre, Email)
            VALUES (@Id, @Nombre, @Email);
            """;

        conexion.Execute(sql, usuario);
    }

    public IReadOnlyList<Usuario> GetUsuarios()
    {
        const string sql = "SELECT Id, Nombre, Email FROM Usuarios WHERE DeletedAt IS NULL ORDER BY Nombre, Id;";

        return conexion.Query<Usuario>(sql).AsList();
    }

    public bool UpdateUsuario(Usuario usuario)
    {
        const string sql = """
            UPDATE Usuarios
            SET Nombre = @Nombre, Email = @Email
            WHERE Id = @Id;
            """;

        return conexion.Execute(sql, usuario) > 0;
    }

    public bool DeleteUsuario(Guid id)
    {
        DateTime deletedAt = DateTime.Now;

        const string sql = """
            UPDATE Usuarios
            SET DeletedAt = @deletedAt
            WHERE Id = @Id;
            """;

        return conexion.Execute(sql, new { DeletedAt = deletedAt, Id = id }) > 0;
    }
}
```

- `Execute(sql, usuario)` ejecuta el `INSERT` o el `UPDATE`. Dapper toma los valores de las propiedades `Id`, `Nombre` y `Email` para los parámetros `@Id`, `@Nombre` y `@Email`, sin concatenarlos en el SQL.
- `Query<Usuario>(sql)` ejecuta el `SELECT` y asigna las columnas a las propiedades de `Usuario`. El filtro `WHERE DeletedAt IS NULL` excluye los usuarios borrados. `AsList()` materializa el resultado como una lista compatible con `IReadOnlyList<Usuario>`.
- En la actualización y el borrado, `Execute(...) > 0` indica si MySQL informó filas afectadas. El servicio utiliza ese resultado para que el endpoint responda con éxito o con HTTP 404.

Las consultas son síncronas. Cuando recibe una conexión cerrada, Dapper la abre para ejecutar la operación y la cierra al terminar. La aplicación no crea tablas ni carga usuarios iniciales automáticamente.

## Configuración de la base de datos

### Crear la base y la tabla

Se necesita un servidor MySQL en ejecución. Ejecutar el archivo `database.sql` desde un cliente de MySQL antes de utilizar los endpoints:

```sql
CREATE DATABASE IF NOT EXISTS usuarios_app
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE usuarios_app;

CREATE TABLE IF NOT EXISTS Usuarios (
    Id CHAR(36) CHARACTER SET ascii COLLATE ascii_bin NOT NULL,
    Nombre VARCHAR(200) NOT NULL,
    Email VARCHAR(254) NOT NULL,
    DeletedAt DATETIME NULL,
    PRIMARY KEY (Id)
) ENGINE=InnoDB;
```

El script crea la base `usuarios_app` y la tabla `Usuarios` si no existen. La tabla comienza vacía; los usuarios se agregan mediante `POST /usuario`. El usuario de conexión debe tener permisos `SELECT`, `INSERT` y `UPDATE` sobre esa tabla; el borrado lógico utiliza `UPDATE`.

Si la tabla ya existía sin la columna `DeletedAt`, ejecutar una vez esta modificación desde un cliente de MySQL con permisos para alterar la tabla. `CREATE TABLE IF NOT EXISTS` no modifica una tabla existente:

```sql
USE usuarios_app;

ALTER TABLE Usuarios ADD COLUMN DeletedAt DATETIME NULL;
```

Los registros existentes quedan con `DeletedAt = NULL`, por lo que siguen apareciendo en `GET /usuario`.

### Configurar appsettings.json

`Presentacion/appsettings.json` define la cadena de conexión dentro de `ConnectionStrings`, con el nombre `MySql`. Ejemplo para una instalación local; reemplazar el usuario y la contraseña por los del servidor:

```json
{
  "ConnectionStrings": {
    "MySql": "Server=localhost;Port=3306;Database=usuarios_app;User ID=tu_usuario;Password=tu_clave;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

| Parámetro | Significado |
| --- | --- |
| `Server` | Dirección del servidor MySQL. |
| `Port` | Puerto de MySQL; en este ejemplo, `3306`. |
| `Database` | Base de datos creada con el script: `usuarios_app`. |
| `User ID` | Usuario de MySQL que utiliza la aplicación. |
| `Password` | Contraseña de ese usuario. |

`WebApplication.CreateBuilder(args)` carga automáticamente `appsettings.json` y luego `appsettings.{Environment}.json`, si existe. En `Development`, los valores de `appsettings.Development.json` sobrescriben las claves coincidentes del archivo base. Actualmente ese archivo solo configura el registro de mensajes, por lo que la cadena de conexión se obtiene de `appsettings.json`.

## Registro de dependencias y almacenamiento

`Presentacion/Program.cs` lee la cadena y relaciona las interfaces con sus implementaciones antes de construir la aplicación:

```csharp
using System.Data;
using Aplicacion.Contratos;
using Aplicacion.Servicios;
using MySqlConnector;
using Persistencia.Contratos;
using Persistencia.Repositorios;

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
```

`GetConnectionString("MySql")` lee la clave `ConnectionStrings:MySql` desde la configuración combinada. Si falta o está vacía, la aplicación detiene el inicio con el mensaje indicado. Esta validación comprueba que exista la cadena; la conexión real con MySQL se realiza al ejecutar una operación del repositorio.

`AddScoped` crea una instancia de cada dependencia por ámbito; en esta API, normalmente por solicitud HTTP. La fábrica de `IDbConnection` construye una `MySqlConnection` con la cadena leída. El contenedor inyecta esa conexión en `UsuarioRepositorio` y el repositorio en `UsuarioServicio`. Al finalizar la solicitud, el contenedor libera la conexión.

```text
UsuarioEndpoints
    → IUsuarioServicio / UsuarioServicio
    → IUsuarioRepositorio / UsuarioRepositorio
    → IDbConnection / MySqlConnection
    → MySQL: usuarios_app.Usuarios
```

Los datos quedan almacenados en MySQL. Un usuario creado por POST puede consultarse en un GET posterior y continúa disponible después de reiniciar la API.

### Ejecutar la API

Con el SDK de .NET 10 instalado, la base creada y la cadena configurada, ejecutar desde la raíz del proyecto:

```powershell
dotnet restore UsuariosApp.slnx
dotnet run --project Presentacion --launch-profile https
```

El perfil `https` activa `Development` y publica la API en `https://localhost:7253` y `http://localhost:5111`. Abrir `https://localhost:7253/swagger` para probarla. Si el entorno local lo requiere, confiar en el certificado de desarrollo con `dotnet dev-certs https --trust`.

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
```

Los endpoints reciben el servicio mediante `[FromServices]`. Se incorporan en `Program.cs` con:

```csharp
app.MapUsuarioEndpoints();
```

Los endpoints no necesitan un registro propio en el contenedor de dependencias.

| Método | Ruta | Entrada | Respuesta |
| --- | --- | --- | --- |
| GET | `/usuario` | Sin cuerpo. | HTTP 200 con una lista de usuarios no borrados en JSON. |
| POST | `/usuario` | JSON con `nombre` y `email`. | HTTP 201 con el usuario creado y el encabezado `Location`. |
| PUT | `/usuario/{id}` | GUID en la ruta y JSON con `nombre` y `email`. | HTTP 200 con el usuario actualizado; HTTP 404 si el repositorio no informa filas afectadas. |
| DELETE | `/usuario/{id}` | GUID en la ruta, sin cuerpo. | HTTP 204 sin cuerpo al marcar el usuario como borrado; HTTP 404 si el repositorio no informa filas afectadas. |

Las rutas de PUT y DELETE usan la restricción `{id:guid}`: el identificador debe tener formato GUID. Una ruta con un identificador que no cumple ese formato no coincide con estos endpoints y devuelve HTTP 404.

### GET /usuario

Consulta los usuarios almacenados en MySQL cuyo `DeletedAt` es `NULL` y devuelve una lista en JSON, ordenada por `Nombre` e `Id`.

```text
GET /usuario
    → UsuarioEndpoints
    → UsuarioServicio.LeerUsuarios()
    → UsuarioRepositorio.GetUsuarios()
    → SELECT en MySQL con WHERE DeletedAt IS NULL y lista de entidades Usuario
    → Respuesta HTTP 200 con JSON
```

Ejemplo de respuesta si se cargaron estos usuarios (la tabla recién creada devuelve `[]`):

```json
[
  { "id": "f47ac10b-58cc-4372-a567-0e02b2c3d479", "nombre": "Juan", "email": "juan@mail.com" },
  { "id": "2c7e8a95-1f34-4d6b-a809-5e2c0f713b68", "nombre": "Leonardo", "email": "leonardo@mail.com" },
  { "id": "9b2d6e31-4c8f-4a70-9d12-8f0e3b6c5a24", "nombre": "Martin", "email": "martin@mail.com" }
]
```

### POST /usuario

El método HTTP **POST** se utiliza aquí para crear un usuario. El cliente envía los datos en el cuerpo de la solicitud con `Content-Type: application/json`.

Ejemplo de solicitud:

```http
POST /usuario
Content-Type: application/json

{
  "nombre": "Ana",
  "email": "ana@mail.com"
}
```

El flujo de creación es:

```text
POST /usuario con JSON
    → UsuarioEndpoints recibe los datos del usuario
    → UsuarioServicio.CrearUsuario()
    → UsuarioRepositorio.CreateUsuario() ejecuta el INSERT en MySQL con Dapper
    → El servicio devuelve los datos del usuario creado
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

### PUT /usuario/{id}

Actualiza el nombre y el email del usuario identificado por el GUID de la ruta. Enviar ambos campos en el cuerpo con `Content-Type: application/json`; el identificador se conserva y no se crea un usuario si no existe.

```http
PUT /usuario/6a1d9c42-3e75-4b80-92f6-7c0e5a18d934
Content-Type: application/json

{
  "nombre": "Ana Pérez",
  "email": "ana.perez@mail.com"
}
```

```text
PUT /usuario/{id} con JSON
    → UsuarioServicio.ActualizarUsuario(id, usuarioDto)
    → UsuarioRepositorio.UpdateUsuario(usuario)
    → UPDATE Usuarios SET Nombre = @Nombre, Email = @Email WHERE Id = @Id
    → HTTP 200 con JSON si hay filas afectadas; HTTP 404 sin cuerpo si no las hay
```

Ejemplo de respuesta exitosa:

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": "6a1d9c42-3e75-4b80-92f6-7c0e5a18d934",
  "nombre": "Ana Pérez",
  "email": "ana.perez@mail.com"
}
```

Si el identificador no existe, el repositorio devuelve `false`, el servicio devuelve `null` y el endpoint responde **404 Not Found**.

### DELETE /usuario/{id}

Marca el usuario como borrado mediante Soft Delete. La solicitud no lleva cuerpo:

```http
DELETE /usuario/6a1d9c42-3e75-4b80-92f6-7c0e5a18d934
```

```text
DELETE /usuario/{id}
    → UsuarioServicio.BorrarUsuario(id)
    → UsuarioRepositorio.DeleteUsuario(id)
    → UPDATE Usuarios SET DeletedAt = @deletedAt WHERE Id = @Id
    → HTTP 204 sin cuerpo si hay filas afectadas; HTTP 404 sin cuerpo si no las hay
```

Ejemplo de respuesta exitosa:

```http
HTTP/1.1 204 No Content
```

Si el identificador no existe, el repositorio devuelve `false` y el endpoint responde **404 Not Found**. Después de un borrado exitoso, el usuario deja de aparecer en `GET /usuario`.

### Soft Delete: borrado lógico

El borrado físico elimina la fila con `DELETE FROM Usuarios`. Esta implementación conserva la fila y usa la columna nullable `DeletedAt` para indicar su estado:

| Valor de `DeletedAt` | Estado | ¿Aparece en GET? |
| --- | --- | --- |
| `NULL` | Usuario activo. | Sí. |
| Fecha y hora | Usuario borrado lógicamente. | No. |

Al crear un usuario no se especifica `DeletedAt`, por lo que queda en `NULL`. Al borrarlo, el repositorio guarda `DateTime.Now` (la fecha y hora local del servidor de la API). La marca se almacena en MySQL y se conserva al reiniciar la aplicación. `DeletedAt` no se expone en los DTOs ni en las respuestas JSON.

Para comprobar el estado desde un cliente de MySQL:

```sql
SELECT Id, Nombre, Email, DeletedAt
FROM Usuarios
WHERE Id = '6a1d9c42-3e75-4b80-92f6-7c0e5a18d934';
```

En el código actual, solamente GET filtra por `DeletedAt IS NULL`. PUT y DELETE buscan por `Id`, incluso si la fila ya fue borrada lógicamente: PUT puede modificar su nombre y email sin reactivarla, y otro DELETE vuelve a asignar la fecha de borrado. Por eso un segundo DELETE no devuelve necesariamente 404. No hay un endpoint de restauración ni de borrado físico.

### Probar desde Swagger

En el entorno `Development`, se habilitan el documento OpenAPI en `/openapi/v1.json` y la interfaz Swagger UI en `/swagger` para explorar y probar los endpoints.

Para probar el ciclo completo:

1. Ejecutar `POST /usuario` con `nombre` y `email`. Comprobar el estado 201 y copiar el `id` de la respuesta.
2. Ejecutar `GET /usuario` y comprobar que el usuario aparece con ese identificador.
3. Ejecutar `PUT /usuario/{id}` con ese GUID y nuevos valores para `nombre` y `email`. Comprobar el estado 200 y verificar los cambios con GET. Al reiniciar la API, los cambios deben seguir disponibles.
4. Ejecutar `DELETE /usuario/{id}` con el mismo GUID. Comprobar el estado 204 y que no haya cuerpo de respuesta.
5. Repetir GET y comprobar que el usuario ya no aparece, incluso después de reiniciar la API. Consultar la tabla en MySQL para verificar que la fila continúa presente con `DeletedAt` informado.
6. Probar PUT y DELETE con un GUID que no exista en la tabla y comprobar que responden 404.
