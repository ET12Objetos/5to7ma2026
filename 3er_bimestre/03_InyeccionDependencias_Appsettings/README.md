# Leer el connection string desde appsettings.json

Esta guía muestra, paso a paso, los cambios necesarios para quitar la cadena de conexión del código fuente, leerla desde `appsettings.json` e inyectarla en los repositorios de una aplicación de consola .NET.

El resultado final tendrá este recorrido:

```text
appsettings.json
        ↓
Presentacion/Program.cs
        ↓
MySqlConnectionFactory
        ↓
Repositorios
        ↓
Servicios y menú
```

En este ejemplo la inyección de dependencias es manual: cada objeto recibe sus dependencias por constructor y `Program.cs` se encarga de crearlas.

## 1. Instalar el paquete NuGet de configuración

Abrir una terminal en la carpeta raíz de la solución y ejecutar:

```bash
dotnet add Presentacion/Presentacion.csproj package Microsoft.Extensions.Configuration.Json
```

Este paquete permite cargar configuración desde archivos JSON y utilizar `ConfigurationBuilder`.

Después de instalarlo, `Presentacion/Presentacion.csproj` debe contener una referencia similar a esta:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="10.0.0" />
</ItemGroup>
```

La versión puede variar según la versión de .NET utilizada por el proyecto.

Para conectarse a MySQL también se necesita `MySqlConnector`. Si el proyecto de persistencia todavía no lo tiene, instalarlo con:

```bash
dotnet add Persistencia/Persistencia.csproj package MySqlConnector
```

## 2. Crear el archivo appsettings.json

Crear el archivo `Presentacion/appsettings.json` con el siguiente contenido:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Port=3306;Database=usuarios_app;User ID=root;Password=pass123;"
  }
}
```

Modificar los valores de acuerdo con la configuración local de MySQL:

- `Server`: servidor de MySQL.
- `Port`: puerto de MySQL; normalmente es `3306`.
- `Database`: nombre de la base de datos.
- `User ID`: usuario de MySQL.
- `Password`: contraseña del usuario.

El nombre `Default` será utilizado en `Program.cs` para recuperar esta cadena.

> La contraseña anterior es solamente un ejemplo. En una aplicación real no se deben subir credenciales al repositorio. Para producción se recomienda usar variables de entorno o un gestor de secretos.

## 3. Configurar la copia de appsettings.json

Al compilar el proyecto, `appsettings.json` debe copiarse al directorio donde se generan los ejecutables.

Agregar el siguiente bloque a `Presentacion/Presentacion.csproj`:

```xml
<ItemGroup>
  <None Update="appsettings.json" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```

Un archivo `Presentacion.csproj` simplificado quedaría así:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="10.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Aplicacion\Aplicacion.csproj" />
    <ProjectReference Include="..\Persistencia\Persistencia.csproj" />
  </ItemGroup>

  <ItemGroup>
    <None Update="appsettings.json" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>

</Project>
```

`PreserveNewest` copia el archivo solamente cuando la versión de origen es más reciente que la que se encuentra en la carpeta de salida.

Este paso es necesario porque la aplicación buscará el archivo, por ejemplo, en:

```text
Presentacion/bin/Debug/net10.0/appsettings.json
```

## 4. Crear el contrato para fabricar conexiones

Crear `Persistencia/IDbConnectionFactory.cs`:

```csharp
using System.Data.Common;

namespace Persistencia;

public interface IDbConnectionFactory
{
    DbConnection CrearConexion();
}
```

Los repositorios dependerán de esta interfaz en lugar de crear directamente una conexión con una cadena escrita en el código.

## 5. Crear la fábrica de conexiones MySQL

Crear `Persistencia/MySqlConnectionFactory.cs`:

```csharp
using System.Data.Common;
using MySqlConnector;

namespace Persistencia;

public sealed class MySqlConnectionFactory(string connectionString)
    : IDbConnectionFactory
{
    private readonly string _connectionString =
        string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentException(
                "La cadena de conexión no puede estar vacía.",
                nameof(connectionString))
            : connectionString;

    public DbConnection CrearConexion()
    {
        return new MySqlConnection(_connectionString);
    }
}
```

La fábrica recibe el connection string por constructor. No lee `appsettings.json` directamente, por lo que la capa de persistencia no queda acoplada al origen de la configuración.

## 6. Inyectar la fábrica en los repositorios

Modificar cada repositorio para que reciba `IDbConnectionFactory` en el constructor.

Por ejemplo, la estructura de `UsuarioRepositorio` debe ser:

```csharp
using Aplicacion.Entidades;
using Aplicacion.Repositorios;
using Dapper;

namespace Persistencia.Repositorios;

public sealed class UsuarioRepositorio(IDbConnectionFactory connectionFactory)
    : IUsuarioRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory =
        connectionFactory
        ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Usuario> ObtenerTodos()
    {
        const string sql = "SELECT * FROM usuarios;";

        using var connection = _connectionFactory.CrearConexion();
        return connection.Query<Usuario>(sql).AsList();
    }
}
```

Aplicar el mismo cambio a los demás repositorios:

- `UsuarioRepositorio`
- `RolRepositorio`
- `PermisoRepositorio`
- `RolPermisoRepositorio`

En cada operación se obtiene una conexión mediante:

```csharp
using var connection = _connectionFactory.CrearConexion();
```

De esta forma ningún repositorio necesita conocer ni almacenar una cadena de conexión propia.

## 7. Leer appsettings.json desde Program.cs

Agregar el espacio de nombres de configuración al inicio de `Presentacion/Program.cs`:

```csharp
using Microsoft.Extensions.Configuration;
```

Después, al comienzo del programa, construir la configuración:

```csharp
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
```

Cada llamada tiene una responsabilidad:

- `SetBasePath(AppContext.BaseDirectory)` indica que el archivo debe buscarse junto a los binarios de la aplicación.
- `AddJsonFile("appsettings.json", optional: false)` carga el archivo y produce un error si no existe.
- `Build()` construye el objeto de configuración.

Leer `ConnectionStrings:Default` y validar que exista:

```csharp
var connectionString = configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'Default' en appsettings.json.");
```

`GetConnectionString("Default")` es una forma abreviada de leer la clave:

```text
ConnectionStrings:Default
```

## 8. Crear e inyectar las dependencias

Utilizar la cadena recuperada para crear `MySqlConnectionFactory`:

```csharp
var connectionFactory = new MySqlConnectionFactory(connectionString);
```

Inyectar la misma fábrica en todos los repositorios:

```csharp
var usuarioRepositorio = new UsuarioRepositorio(connectionFactory);
var rolRepositorio = new RolRepositorio(connectionFactory);
var permisoRepositorio = new PermisoRepositorio(connectionFactory);
var rolPermisoRepositorio = new RolPermisoRepositorio(connectionFactory);
```

Luego continuar con las dependencias de la aplicación: los repositorios se inyectan en los servicios y los servicios en el menú.

```csharp
var usuarioServicio = new UsuarioServicio(usuarioRepositorio);
var rolServicio = new RolServicio(rolRepositorio);
var permisoServicio = new PermisoServicio(permisoRepositorio);
var rolPermisoServicio = new RolPermisoServicio(rolPermisoRepositorio);

var menu = new ConsoleMenu(
    usuarioServicio,
    rolServicio,
    permisoServicio,
    rolPermisoServicio);

menu.Ejecutar();
```

## 9. Código completo de Program.cs

Al finalizar, `Presentacion/Program.cs` debe quedar así:

```csharp
using Aplicacion.Servicios;
using Microsoft.Extensions.Configuration;
using Persistencia;
using Persistencia.Repositorios;
using Presentacion;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'Default' en appsettings.json.");

var connectionFactory = new MySqlConnectionFactory(connectionString);

var usuarioRepositorio = new UsuarioRepositorio(connectionFactory);
var rolRepositorio = new RolRepositorio(connectionFactory);
var permisoRepositorio = new PermisoRepositorio(connectionFactory);
var rolPermisoRepositorio = new RolPermisoRepositorio(connectionFactory);

var usuarioServicio = new UsuarioServicio(usuarioRepositorio);
var rolServicio = new RolServicio(rolRepositorio);
var permisoServicio = new PermisoServicio(permisoRepositorio);
var rolPermisoServicio = new RolPermisoServicio(rolPermisoRepositorio);

var menu = new ConsoleMenu(
    usuarioServicio,
    rolServicio,
    permisoServicio,
    rolPermisoServicio);

menu.Ejecutar();
```

## 10. Compilar y ejecutar

Desde la carpeta raíz de la solución, restaurar los paquetes y compilar:

```bash
dotnet restore UsuariosApp.slnx
dotnet build UsuariosApp.slnx
```

Ejecutar la aplicación:

```bash
dotnet run --project Presentacion/Presentacion.csproj
```

## Archivos modificados o creados

| Archivo | Cambio |
|---|---|
| `Presentacion/Presentacion.csproj` | Agregar el paquete de configuración y copiar `appsettings.json`. |
| `Presentacion/appsettings.json` | Guardar `ConnectionStrings:Default`. |
| `Presentacion/Program.cs` | Cargar la configuración, leer la cadena y construir las dependencias. |
| `Persistencia/IDbConnectionFactory.cs` | Definir el contrato para crear conexiones. |
| `Persistencia/MySqlConnectionFactory.cs` | Recibir la cadena por constructor y crear conexiones MySQL. |
| `Persistencia/Repositorios/*.cs` | Recibir y utilizar `IDbConnectionFactory`. |

## Errores frecuentes

### No se encontró appsettings.json

Comprobar que `Presentacion.csproj` incluya:

```xml
<None Update="appsettings.json" CopyToOutputDirectory="PreserveNewest" />
```

También se puede revisar si el archivo fue copiado a `Presentacion/bin/Debug/net10.0/`.

### No se encontró la cadena Default

El nombre debe coincidir exactamente en ambos lugares.

En `appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "..."
}
```

En `Program.cs`:

```csharp
configuration.GetConnectionString("Default");
```

### Error al conectarse a MySQL

Verificar que:

- MySQL esté iniciado.
- La base de datos exista.
- El servidor y el puerto sean correctos.
- El usuario tenga permisos sobre la base de datos.
- La contraseña sea correcta.

Con estos cambios, la cadena de conexión queda centralizada en `appsettings.json`, la capa de persistencia recibe la configuración por inyección de constructor y los repositorios dejan de contener datos de conexión escritos directamente en el código.
